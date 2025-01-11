using System;
using BrainCells.Application.Common;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.Common;
using BrainCells.Presentation.Models.Account.Validators;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Presentation.Controllers;

[Route("[controller]")]
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

    [Route("SignIn")]
    [HttpGet]
    public ActionResult SignIn()
    {
        return View("SignIn");
    }

    [Route("SignIn")]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromForm]SigninViewModel account)
    {
        var validate = _signinValidator.Validate(account);
        if(validate.IsValid)
        {
            var x = await _accountRepository.SignIn(account.Email,account.Password,account.Persistent);
            ModelState.AddModelError("", x.Message);
        }
        else
            ModelState.AddFluentResult(validate);

        return View("SignIn", account);
    }
}