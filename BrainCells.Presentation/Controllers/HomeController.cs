using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrainCells.Presentation.Models;

namespace BrainCells.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger _logger;

    public HomeController(ILoggerFactory logger)
    {
        _logger = logger.CreateLogger("Home");
  
    }

    public IActionResult Index()
    {
       
        return View();
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
