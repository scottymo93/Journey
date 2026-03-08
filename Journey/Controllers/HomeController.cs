using System.Diagnostics;
using Journey.Data;
using Microsoft.AspNetCore.Mvc;
using Journey.Models;

namespace Journey.Controllers;

public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;
    //
    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    private readonly IPinRepository _pinRepository;

    public HomeController(IPinRepository pinRepository)
    {
        _pinRepository = pinRepository;
    }
    
    public IActionResult Index()
    {
        var pins = _pinRepository.GetAllPins();
        return View(pins);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}