using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Presentation.Models.Account.ViewModels;
using System.Security.Claims;
using BrainCells.Presentation.Models.Home.ViewModels;
using BrainCells.Application.Services.ContactService;
using FluentValidation;
using BrainCells.Application.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BrainCells.Presentation.Controllers;

[Authorize]
[Controller]
public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IContactService _contactService;
        private readonly IValidator<ContactViewModel> _saveMessageValidator;
    private readonly IAccountRepository _accountRepository;

    public HomeController(ILoggerFactory logger, 
        IContactService contactService,
            IValidator<ContactViewModel> saveMessageValidator,
        IAccountRepository accountRepository)
    {
        _logger = logger.CreateLogger("BrainCells");
        _contactService = contactService;
            _saveMessageValidator = saveMessageValidator;
        _accountRepository = accountRepository;
    } 

    private async Task<bool> setAccount()
    {
        var account = await _accountRepository.GetAccountAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if(account == null)
            return false;
        ViewData["Account"] = new AccountViewModel{
            Id = account.Id,
            Email = account.Email,
            Role = account.Role,
            Name = account.Name,
            Picture = account.Picture,
        };
        return true;
    }

    [Authorize]
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {   
        await setAccount();
        return View("Index");
    }

    [Route("Terms")]
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Terms()
    {
        return View("Terms");
    }

    [Route("Contact")]
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Contact()
    {
        return View("Contact");
    }

    [Route("Contact")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Contact([FromForm]ContactViewModel contact)
    {
        var validate = _saveMessageValidator.Validate(contact);
        if(validate.IsValid)
        {
            var result = await _contactService.SaveMessageAsync(contact.FullName, contact.Email, contact.Message);
            if(result.Success)
            {
                ModelState.Clear();
                ViewData["MessageType"] = AppConsts.SUCCESS;
            }
            else
                ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("Contact", result.Message);
        }
        else
        {
            ModelState.AddFluentResult(validate);
            ViewData["MessageType"]= AppConsts.WARNING;
        }
        return View("Contact");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
