using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProtocoloRural.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProtocoloRural.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("[controller]")]
    public class IndicadoresController : Controller
    {
        private IMongoCollection<Indicador> GetCollection()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(ContextMongodb.ConnectionString));
            if (ContextMongodb.IsSSL)
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var db = client.GetDatabase(ContextMongodb.Database);
            return db.GetCollection<Indicador>("Indicadores");
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var col = GetCollection();
            var list = await col.Find(Builders<Indicador>.Filter.Empty).SortBy(i => i.Nome).ToListAsync();
            return View(list);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Indicador model)
        {
            // Garante que só vão para o banco os parâmetros preenchidos com texto! 
            if (model.Parametros == null)
                model.Parametros = new List<Parametro>();

            // Filtros: pega só os Textos preenchidos e no máximo 6
            model.Parametros = model.Parametros
                .Where(p => !string.IsNullOrWhiteSpace(p.Texto))
                .Take(6)
                .ToList();

            if (model.Parametros.Count < 2)
            {
                TempData["Error"] = "Informe pelo menos 2 parâmetros para o indicador.";
                return RedirectToAction(nameof(Index));
            }

            // Ajusta valores antes de gravar (valor = ordem)
            for (int i = 0; i < model.Parametros.Count; i++)
                model.Parametros[i].Valor = i;

            model.CriadoEm = DateTime.UtcNow;
            var col = GetCollection();
            await col.InsertOneAsync(model);
            TempData["Message"] = "Indicador criado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var col = GetCollection();
            var filtro = Builders<Indicador>.Filter.Eq(i => i.Id, id);
            var ent = await col.Find(filtro).FirstOrDefaultAsync();
            if (ent == null) return NotFound();
            return View(ent);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Indicador model)
        {
            if (model.Parametros == null)
                model.Parametros = new List<Parametro>();
            model.Parametros = model.Parametros
                .Where(p => !string.IsNullOrWhiteSpace(p.Texto))
                .Take(6)
                .ToList();

            if (model.Parametros.Count < 2)
            {
                TempData["Error"] = "Informe pelo menos 2 parâmetros para o indicador.";
                return RedirectToAction(nameof(Index));
            }

            model.Id = id;
            model.AtualizadoEm = DateTime.UtcNow;
            for (int i = 0; i < model.Parametros.Count; i++)
                model.Parametros[i].Valor = i;

            var col = GetCollection();
            var filtro = Builders<Indicador>.Filter.Eq(i => i.Id, id);
            await col.ReplaceOneAsync(filtro, model);
            TempData["Message"] = "Indicador atualizado.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var col = GetCollection();
            var filtro = Builders<Indicador>.Filter.Eq(i => i.Id, id);
            await col.DeleteOneAsync(filtro);
            TempData["Message"] = "Indicador removido.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("toggle/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(string id)
        {
            var col = GetCollection();
            var filtro = Builders<Indicador>.Filter.Eq(i => i.Id, id);
            var ent = await col.Find(filtro).FirstOrDefaultAsync();
            if (ent == null) return NotFound();
            ent.Estado = !ent.Estado;
            ent.AtualizadoEm = DateTime.UtcNow;
            await col.ReplaceOneAsync(filtro, ent);
            TempData["Message"] = "Estado alterado.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var col = GetCollection();
            var filtro = Builders<Indicador>.Filter.Eq(i => i.Id, id);
            var ent = await col.Find(filtro).FirstOrDefaultAsync();
            if (ent == null) return NotFound();
            return View(ent);
        }
    }
}