using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProtocoloRural.Models;

namespace ProtocoloRural.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("sobre")]
    public IActionResult Sobre()
    {
        return View();
    }

    [HttpGet("quemsomos")]
    public IActionResult QuemSomos()
    {
        return View();
    }

    [HttpGet("contato")]
    public IActionResult Contato()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}