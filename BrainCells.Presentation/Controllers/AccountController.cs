using System;
using System.Drawing;
using System.Security.Claims;
using BrainCells.Application.Common;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.Common;
using BrainCells.Presentation.Models.Account.Validators;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tAccount = await _accountRepository.ViewAccountAsync(
            User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());
        AccountViewModel account = new AccountViewModel{
            Id = tAccount.Id,
            Email = tAccount.Email,
            Role = tAccount.Role,
            Name = tAccount.Name,
            Picture = tAccount.Picture,
        };
        return View("Index", account);
    }

    [Route("SignIn")]
    [HttpGet]
    public ActionResult SignIn()
    {
        ViewData["Message"] = AppConsts.NONE;
        return View("SignIn");
    }

    [Route("SignIn")]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromForm]SigninViewModel account)
    {
        var validate = _signinValidator.Validate(account);
        if(validate.IsValid)
        {
            var result = await _accountRepository.SignInAsync(account.Email, account.Password, account.Persistent);
            if(result.Success)
            {
                ViewData["Message"] = AppConsts.SUCCESS;
                ModelState.AddModelError("SignIn", result.Message);
            }
            else
            {
                ViewData["Message"] = AppConsts.ERROR;
                ModelState.AddModelError("SignIn", result.Message);
            }  
        }
        else
        {
            ViewData["Message"] = AppConsts.WARNING;
            ModelState.AddFluentResult(validate);
        }
        return View("SignIn", account);
    }

    [Route("SignUp")]
    [HttpGet]
    public IActionResult SignUp()
    {
        ViewData["Message"] = AppConsts.NONE;
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
                ViewData["Message"] = AppConsts.SUCCESS;
                ModelState.AddModelError("SignUp", result.Message);
            }
            else
            {
                ViewData["Message"] = AppConsts.ERROR;
                ModelState.AddModelError("SignUp", result.Message);
            }
        }
        else
        {
            ViewData["Message"] = AppConsts.WARNING;
            ModelState.AddFluentResult(validate);
        }
        return View("SignUp", account);
    }

    [Route("SignOut")]
    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        var result = await _accountRepository.SignOutAsync();
        if(result.Success)
        {
            ViewData["Message"] = AppConsts.SUCCESS;
            ModelState.AddModelError("SignUp", result.Message);
        }
        else
        {
            ViewData["Message"]= AppConsts.ERROR;
            ModelState.AddModelError("SignUp", result.Message);
        }
        return View("SignIn");
    }
}