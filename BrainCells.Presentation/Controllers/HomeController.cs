using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrainCells.Presentation.Models;

namespace BrainCells.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILoggerFactory logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger.CreateLogger("Home");
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        var obj = _httpContextAccessor.HttpContext;
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
