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
    private readonly IValidator<SignupViewModel> _signupValidator;

    public AccountController(IAccountRepository accountRepository,
        IValidator<SigninViewModel> signinValidator, IValidator<SignupViewModel> signupValidator)
    {
        _accountRepository = accountRepository;
        _signinValidator = signinValidator;
        _signupValidator = signupValidator;
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
           
        }
        else
        {
            ModelState.AddFluentResult(validate);
        }
        return View("SignIn", account);
    }

    [Route("SignUp")]
    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        
        return View("SignUp");
    }

    [Route("SignUp")]
    [HttpPost]
    public async Task<IActionResult> SignUp([FromForm]SignupViewModel account)
    {
        var validate = _signupValidator.Validate(account);
        if(validate.IsValid)
        {
            var result = await _accountRepository.SignUpAsync(new SignUpDto{
                Email = account.Email,
                Password = account.Password,
                Name = account.Name,
            });
            if(result.Success)
            {
                ViewBag.hasMessage = "yes";
                ModelState.AddModelError("SignUp", result.Message);
            }
            else{
                ViewBag.hasMessage = "yes";
                ModelState.AddModelError("SignUp", result.Message);
            }

        }
        else
        {
            ViewBag.hasMessage = "yes";
            ModelState.AddFluentResult(validate);
        }
        return View("SignUp", account);
    }
}