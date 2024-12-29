using System;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Presentation.Controllers;

[Route("[controller]")]
[Controller]
public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var result = _accountRepository.SignUp(new SignUpDto {
            Email="hamed.damaavandi@gmail.com",
            Password="123456",
            Name="Hamed Damavandi",
        });

        return View("Index",result);
    }

    [Route("signup")]
    [HttpGet]
    public IActionResult SignUp()
    {

        
        return View("SignUp");
    }


}