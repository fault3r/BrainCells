using System;
using System.Drawing;
using System.Security.Claims;
using BrainCells.Application.Common;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.SupportEmailService;
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
    private readonly ISupportEmailService _supportEmailService;
    private readonly IAccountRepository _accountRepository;
    private readonly IValidator<SigninViewModel> _signinValidator;
    private readonly IValidator<SignupViewModel> _signupValidator;

    public AccountController(ISupportEmailService supportEmailService, IAccountRepository accountRepository,
        IValidator<SigninViewModel> signinValidator, IValidator<SignupViewModel> signupValidator)
    {
        _supportEmailService = supportEmailService;
        _accountRepository = accountRepository;
        _signinValidator = signinValidator;
        _signupValidator = signupValidator;
    }

    [Route("SignIn")]
    [HttpGet]
    public ActionResult SignIn()
    {
        ViewData["MessageType"] = AppConsts.NONE;
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
                ViewData["MessageType"] = AppConsts.SUCCESS;
                return Redirect("/");
            }
            else
                ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("SignIn", result.Message);
        }
        else
        {
            ViewData["MessageType"] = AppConsts.WARNING;
            ModelState.AddFluentResult(validate);
        }
        return View("SignIn", account);
    }

    [Route("SignUp")]
    [HttpGet]
    public IActionResult SignUp()
    {
        ViewData["MessageType"] = AppConsts.NONE;
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
                ViewData["MessageType"] = AppConsts.SUCCESS;
                await _accountRepository.SignInAsync(account.Email, account.Password, true);
                return Redirect("/");   
            }
            else
                ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("SignUp", result.Message);
        }
        else
        {
            ViewData["MessageType"] = AppConsts.WARNING;
            ModelState.AddFluentResult(validate);
        }
        return View("SignUp", account);
    }

    [Authorize]
    [Route("SignOut")]
    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        var result = await _accountRepository.SignOutAsync(User.FindFirst(ClaimTypes.Email).Value.ToString());
        if(result.Success)
            ViewData["MessageType"] = AppConsts.SUCCESS;        
        else
            ViewData["MessageType"]= AppConsts.ERROR;
        ModelState.AddModelError("SignOut", result.Message);
        return View("SignIn");
    }

    [Route("ForgotPassword")]
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View("ForgotPassword");
    }

    [Route("ForgotPassword")]
    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromForm]string email)
    {
        var result = await _accountRepository.ForgotPasswordAsync(email);   
        if(result.Success)
            ViewData["MessageType"] = AppConsts.SUCCESS;        
        else
            ViewData["MessageType"]= AppConsts.ERROR;
        ModelState.AddModelError("ForgotPassword", result.Message);
        return View("ForgotPassword");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
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


}