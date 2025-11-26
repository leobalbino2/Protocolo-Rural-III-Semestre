using Microsoft.AspNetCore.Mvc;
using ProtocoloRural.Models;
using ProtocoloRural.ViewModels;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProtocoloRural.Controllers
{
    [Authorize]
    public class ResultadosController : Controller
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

        private IMongoCollection<Indicador> GetIndicadorCollection()
        {
            var client = CreateClient();
            var db = client.GetDatabase(ContextMongodb.Database);

            var collections = db.ListCollectionNames().ToList();
            var found = collections.FirstOrDefault(n => string.Equals(n, "indicadores", StringComparison.OrdinalIgnoreCase));
            var collectionName = found ?? "indicadores";
            return db.GetCollection<Indicador>(collectionName);
        }

        // GET: /Resultados/Index?id={id}
        [HttpGet]
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
            var indicadorIds = avaliacao.Respostas.Select(r => r.IndicadorId).Distinct().ToList();
            var indicadores = indicadorIds.Any()
                ? colI.Find(Builders<Indicador>.Filter.In(x => x.Id, indicadorIds)).ToList()
                : new List<Indicador>();

            var respostasDisplay = new List<RespostaDisplayVm>();
            foreach (var r in avaliacao.Respostas)
            {
                var indicador = indicadores.FirstOrDefault(i => i.Id == r.IndicadorId);
                string indicadorNome = indicador?.Nome ?? "Indicador";
                string parametroTexto = string.Empty;

                if (indicador != null && r.ParametroId >= 0 && r.ParametroId < indicador.Parametros.Count)
                {
                    parametroTexto = indicador.Parametros[r.ParametroId].Texto;
                }

                respostasDisplay.Add(new RespostaDisplayVm
                {
                    IndicadorId = r.IndicadorId,
                    IndicadorNome = indicadorNome,
                    ParametroId = r.ParametroId,
                    ParametroTexto = parametroTexto,
                    Valor = r.Valor,
                    Texto = r.Texto
                });
            }

            var vm = new ResultadosViewModel
            {
                Avaliacao = avaliacao,
                Indicadores = indicadores,
                Respostas = respostasDisplay
            };

            // retorna a view Index em Views/Resultados/Index.cshtml
            return View(vm);
        }
    }
}