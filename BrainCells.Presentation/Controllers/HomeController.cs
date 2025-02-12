using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Presentation.Models.Account.ViewModels;
using System.Security.Claims;
using BrainCells.Presentation.Models.ViewModels;
using BrainCells.Presentation.Models.Home.ViewModels;

namespace BrainCells.Presentation.Controllers;

[Authorize]
[Controller]
public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IAccountRepository _accountRepository;


    public HomeController(ILoggerFactory logger, IAccountRepository accountRepository)
    {
        _logger = logger.CreateLogger("BrainCells");
        _accountRepository = accountRepository;
    }

    [Authorize]
    [HttpGet]
    public async  Task<IActionResult> Index()
    {   
        ViewData["Account"] = await viewAccount() as AccountViewModel;
        return View("Index");
    }

    private async Task<AccountViewModel> viewAccount()
    {
        var account = await _accountRepository.ViewAccountAsync(
            User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());
        if(account != null)
            return new AccountViewModel{
                Id = account.Id,
                Email = account.Email,
                Role = account.Role,
                Name = account.Name,
                Picture = account.Picture,
            };
        else
            return null;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Terms()
    {
        return View("Terms");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Contact()
    {
        return View("Contact");
    }
    
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Contact([FromForm]ContactViewModel contact)
    {
        return View("Contact");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
