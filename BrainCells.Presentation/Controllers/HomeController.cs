using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrainCells.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using BrainCells.Application.Common;

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
    [HttpGet]
    public IActionResult Index()
    {
        string s = OnetimePassword.Create();
        string sa = OnetimePassword.Create();
        string saa = OnetimePassword.Create();
        string saaa = OnetimePassword.Create();
        
        _logger.LogInformation("***It's fault3r, Inc. WELCOME..0;");
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
