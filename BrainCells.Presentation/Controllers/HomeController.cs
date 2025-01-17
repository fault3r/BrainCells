using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrainCells.Presentation.Models;
using Microsoft.AspNetCore.Authorization;

namespace BrainCells.Presentation.Controllers;

[Controller]
public class HomeController : Controller
{
    private readonly ILogger _logger;

    public HomeController(ILoggerFactory logger)
    {
        _logger = logger.CreateLogger("Home");
    }

    [Authorize]
    public IActionResult Index()
    {
        _logger.LogInformation("***It's fault3r, Inc. WELCOME..0;");
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
