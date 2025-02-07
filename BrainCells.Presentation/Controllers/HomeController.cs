using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrainCells.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Presentation.Models.Account.ViewModels;
using System.Security.Claims;

namespace BrainCells.Presentation.Controllers;

[Controller]
public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IAccountRepository _accountRepository;


    public HomeController(ILoggerFactory logger, IAccountRepository accountRepository)
    {
        _logger = logger.CreateLogger("Home");
        _accountRepository = accountRepository;
    }

    [Authorize]
    [HttpGet]
    public async  Task<IActionResult> Index()
    {   
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
