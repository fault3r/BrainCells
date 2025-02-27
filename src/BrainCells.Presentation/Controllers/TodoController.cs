using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Presentation.Models.Account.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Presentation.Controllers;

[Route("[controller]")]
[Controller]
public class TodoController : Controller
{
    private readonly IAccountRepository _accountRepository;

    public TodoController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    private async Task setAccount()
    {
        var account = await _accountRepository.GetAccountAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        ViewData["Account"] = new AccountViewModel{
            Id = account.Id,
            Email = account.Email,
            Role = account.Role,
            Name = account.Name,
            Picture = account.Picture,
        };
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await setAccount();
        return View("Index");
    }
}