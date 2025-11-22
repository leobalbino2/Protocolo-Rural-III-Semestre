using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProtocoloRural.Controllers
{
    public class IndicadoresController : Controller
    {
        private readonly ILogger<IndicadoresController> _logger;

        public IndicadoresController(ILogger<IndicadoresController> logger)
        {
            _logger = logger;
        }

        [HttpGet("indicadores")]
        public IActionResult Index()
        {
            return View();
        }
    }
}