using Microsoft.AspNetCore.Mvc;
using ProtocoloRural.Models;
using ProtocoloRural.ViewModels;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProtocoloRural.Controllers
{
    [Authorize]
    public class AvaliacaoController : Controller
    {
        private MongoClient CreateClient()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(ContextMongodb.ConnectionString));
            if (ContextMongodb.IsSSL)
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

            return new MongoClient(settings);
        }

        private IMongoCollection<Avaliacao> GetAvaliacaoCollection()
        {
            var client = CreateClient();
            var db = client.GetDatabase(ContextMongodb.Database);
            return db.GetCollection<Avaliacao>("avaliacoes");
        }

        // Busca a collection de indicadores tentando respeitar o nome real no banco (case-insensitive)
        private IMongoCollection<Indicador> GetIndicadorCollection()
        {
            var client = CreateClient();
            var db = client.GetDatabase(ContextMongodb.Database);

            // Lista os nomes existentes e tenta achar a collection "indicadores" ignorando case
            var collections = db.ListCollectionNames().ToList();
            var found = collections.FirstOrDefault(n => string.Equals(n, "indicadores", StringComparison.OrdinalIgnoreCase));

            var collectionName = found ?? "indicadores"; // se não encontrar, usa "indicadores" como fallback
            return db.GetCollection<Indicador>(collectionName);
        }

        // GET: /Avaliacao/Index?id={id}
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index", "Painel");

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var colA = GetAvaliacaoCollection();
            var avaliacao = colA.Find(
                Builders<Avaliacao>.Filter.And(
                    Builders<Avaliacao>.Filter.Eq(x => x.Id, id),
                    Builders<Avaliacao>.Filter.Eq(x => x.UsuarioId, usuarioId)
                )
            ).FirstOrDefault();

            if (avaliacao == null)
            {
                TempData["Error"] = "Avaliação não encontrada ou você não tem acesso.";
                return RedirectToAction("Index", "Painel");
            }

            var colI = GetIndicadorCollection();
            var indicadores = colI.Find(Builders<Indicador>.Filter.Eq(x => x.Estado, true))
                                  .SortBy(x => x.Nome)
                                  .ToList();

            ViewBag.IndicadoresCount = indicadores?.Count ?? 0;
            ViewBag.IndicadorCollectionName = (colI.CollectionNamespace?.CollectionName) ?? "indicadores";

            var indicadorVms = indicadores.Select(i => new IndicadorVm
            {
                Id = i.Id ?? string.Empty,
                Nome = i.Nome,
                Descricao = i.Descricao,
                Parametros = i.Parametros.Select((p, idx) => new ParametroVm
                {
                    Index = idx,
                    Texto = p.Texto,
                    Valor = p.Valor
                }).ToList()
            }).ToList();

            var vm = new AvaliacaoViewModel
            {
                Avaliacao = avaliacao,
                Indicadores = indicadorVms
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SalvarRespostas()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var form = Request.Form;

            var avaliacaoId = form["avaliacao_id"].FirstOrDefault();
            if (string.IsNullOrEmpty(avaliacaoId))
            {
                TempData["Error"] = "Avaliação inválida.";
                return RedirectToAction("Index", "Painel");
            }

            // Recarregar indicadores ativos (os mesmos que foram renderizados)
            var colI = GetIndicadorCollection();
            var indicadores = colI.Find(Builders<Indicador>.Filter.Eq(x => x.Estado, true)).ToList();

            var respostas = new List<RespostaAvaliacao>();
            double totalPoints = 0;
            double maxPoints = 0;

            foreach (var key in form.Keys)
            {
                if (!key.StartsWith("indicador_")) continue;

                var indicadorId = key.Substring("indicador_".Length);
                var selectedValue = form[key].FirstOrDefault(); // índice do parâmetro como string

                if (string.IsNullOrEmpty(selectedValue)) continue;

                if (!int.TryParse(selectedValue, out int parametroIndex)) continue;

                var indicador = indicadores.FirstOrDefault(i => i.Id == indicadorId);
                if (indicador == null) continue;

                if (parametroIndex < 0 || parametroIndex >= indicador.Parametros.Count) continue;

                var parametro = indicador.Parametros[parametroIndex];
                var resposta = new RespostaAvaliacao
                {
                    IndicadorId = indicador.Id ?? string.Empty,
                    ParametroId = parametroIndex,
                    Valor = parametro.Valor,
                    Texto = null
                };
                respostas.Add(resposta);

                totalPoints += parametro.Valor;
                var maxValor = indicador.Parametros.Max(p => p.Valor);
                maxPoints += maxValor;
            }

            double porcentagem = 0;
            int grau = 0;
            if (maxPoints > 0)
            {
                porcentagem = (totalPoints / maxPoints) * 100.0;
                if (porcentagem <= 20) grau = 1;
                else if (porcentagem <= 40) grau = 2;
                else if (porcentagem <= 60) grau = 3;
                else if (porcentagem <= 80) grau = 4;
                else grau = 5;
            }

            var colA = GetAvaliacaoCollection();

            var filter = Builders<Avaliacao>.Filter.And(
                Builders<Avaliacao>.Filter.Eq(x => x.Id, avaliacaoId),
                Builders<Avaliacao>.Filter.Eq(x => x.UsuarioId, usuarioId)
            );

            var update = Builders<Avaliacao>.Update
                .Set(x => x.Respostas, respostas)
                .Set(x => x.Finalizado, true)
                .Set(x => x.GrauSustentabilidade, grau)
                .Set(x => x.GrauPorcentagem, porcentagem)
                .Set(x => x.DataAvaliacao, DateTime.UtcNow);

            var result = colA.UpdateOne(filter, update);

            if (result.ModifiedCount > 0)
            {
                // redireciona para ResultadosController.Index (controller separado)
                return RedirectToAction("Index", "Resultados", new { id = avaliacaoId });
            }
            else
            {
                TempData["Error"] = "Erro ao salvar a avaliação. Verifique se você tem acesso.";
                return RedirectToAction("Index", "Painel");
            }
        }

        [HttpGet]
        public IActionResult ListCollections()
        {
            var client = CreateClient();
            var db = client.GetDatabase(ContextMongodb.Database);
            var names = db.ListCollectionNames().ToList();
            return Json(new { database = ContextMongodb.Database, collections = names });
        }
    }
}