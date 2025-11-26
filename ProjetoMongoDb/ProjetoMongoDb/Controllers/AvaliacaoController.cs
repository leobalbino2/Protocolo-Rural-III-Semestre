using Microsoft.AspNetCore.Mvc;
using ProtocoloRural.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProtocoloRural.Controllers
{
    [Authorize]
    public class AvaliacaoController : Controller
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

        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index", "Painel");

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var col = GetCollection();
            var avaliacao = col.Find(
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

            return View(avaliacao);
        }
    }
}