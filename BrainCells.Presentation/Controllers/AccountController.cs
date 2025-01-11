using System;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.Common;
using BrainCells.Presentation.Models.Account.Validators;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Presentation.Controllers;

[Controller]
public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IValidator<SigninViewModel> _signinValidator;

    public AccountController(IAccountRepository accountRepository, IValidator<SigninViewModel> signinValidator)
    {
        _accountRepository = accountRepository;
        _signinValidator = signinValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View("Index");
    }

    [Route("signin")]
    [HttpPost]
    public IActionResult SignIn([FromForm]SigninViewModel account)
    {
        var val = _signinValidator.Validate(account);


        foreach(var item in val.Errors)
            ModelState.AddModelError("",item.ErrorMessage);
    

        return View("Index");
    }
}