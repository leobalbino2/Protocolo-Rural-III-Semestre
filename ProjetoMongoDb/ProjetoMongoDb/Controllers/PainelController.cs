using Microsoft.AspNetCore.Mvc;
using ProtocoloRural.Models;
using ProtocoloRural.ViewModels;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProtocoloRural.Controllers
{
    [Authorize]
    public class PainelController : Controller
    {
        private IMongoCollection<Avaliacao> GetCollection()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(ContextMongodb.ConnectionString));
            if (ContextMongodb.IsSSL)
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var db = client.GetDatabase(ContextMongodb.Database);
            return db.GetCollection<Avaliacao>("avaliacoes");
        }

        public IActionResult Index()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var col = GetCollection();
            var avaliacoes = col.Find(Builders<Avaliacao>.Filter.Eq(x => x.UsuarioId, usuarioId))
                .SortByDescending(x => x.DataAvaliacao)
                .ToList();

            var vm = new PainelViewModel
            {
                Avaliacoes = avaliacoes,
                Mensagem = TempData["Message"] as string
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarAvaliacao(string nome_propriedade)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var novaAvaliacao = new Avaliacao
            {
                UsuarioId = usuarioId, 
                NomePropriedade = nome_propriedade.Trim(),
                DataAvaliacao = DateTime.UtcNow,
                GrauSustentabilidade = 0,
                GrauPorcentagem = 0,
                Finalizado = false,
                Respostas = new List<RespostaAvaliacao>()
            };

            var col = GetCollection();
            col.InsertOne(novaAvaliacao);

            TempData["Message"] = "Avaliação criada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoverAvaliacao(string id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var col = GetCollection();

            var deleteResult = col.DeleteOne(
                Builders<Avaliacao>.Filter.And(
                    Builders<Avaliacao>.Filter.Eq(x => x.Id, id),
                    Builders<Avaliacao>.Filter.Eq(x => x.UsuarioId, usuarioId)
                )
            );

            TempData["Message"] = deleteResult.DeletedCount > 0
                ? "Avaliação removida com sucesso!"
                : "Erro ao remover a avaliação.";

            return RedirectToAction("Index");
        }
    }
}