using System;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly IValidator<ChangePasswordViewModel> _changePasswordValidator;
        private readonly IValidator<EditInformationViewModel> _editInformationValidator;

    public AccountController(ISupportEmailService supportEmailService,
        IAccountRepository accountRepository,
            IValidator<SigninViewModel> signinValidator,
            IValidator<SignupViewModel> signupValidator,
            IValidator<ChangePasswordViewModel> changePasswordValidator,
            IValidator<EditInformationViewModel> editInformationValidator)
    {
        _supportEmailService = supportEmailService;
        _accountRepository = accountRepository;
            _signinValidator = signinValidator;
            _signupValidator = signupValidator;
            _changePasswordValidator = changePasswordValidator;
            _editInformationValidator = editInformationValidator;
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
        ModelState.Clear();
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
        return View("SignUp");
    }

    [Route("SignUp")]
    [HttpPost]
    public async Task<IActionResult> SignUp([FromForm]SignupViewModel account)
    {
        ModelState.Clear();
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
                await _accountRepository.SignInAsync(account.Email, account.Password, true);
                ViewData["MessageType"] = AppConsts.SUCCESS;
                return Redirect("/");   
            }
            else
                ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("SignUp", result.Message);
        }
        else
        {
            ModelState.AddFluentResult(validate);
            ViewData["MessageType"] = AppConsts.WARNING;
        }
        return View("SignUp", account);
    }

    [Authorize]
    [Route("SignOut/{ReturnUrl:bool?}")]
    [HttpGet]
    public async Task<IActionResult> SignOut(bool? ReturnUrl)
    {
        ModelState.Clear();
        var result = await _accountRepository.SignOutAsync(User.FindFirstValue(ClaimTypes.Email).ToString());
        if(result.Success)
        {
            if(ReturnUrl == true)
            {
                ViewData["MessageType"] = AppConsts.WARNING;
                result.Message = "Your session has expired! For your security, please log in again to continue..";
            }
            else
                ViewData["MessageType"] = AppConsts.SUCCESS;   
        } 
        else
            ViewData["MessageType"]= AppConsts.ERROR;
        ModelState.AddModelError("SignOut", result.Message);
        return View("SignIn");
    }
    private async Task setAccount()
    {
        var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(accountId != null)
        {
            var account = await _accountRepository.GetAccountAsync(accountId);
            if(account != null)
                ViewData["Account"] = new AccountViewModel{
                    Id = account.Id,
                    Email = account.Email,
                    Role = account.Role,
                    Name = account.Name,
                    Picture = account.Picture,
                };
            else
                ViewData["Account"] = null;
        }
        else
            ViewData["Account"] = null;
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
        ModelState.Clear();
        var result = await _accountRepository.ForgotPasswordAsync(email);   
        if(result.Success)
            ViewData["MessageType"] = AppConsts.SUCCESS;        
        else
            ViewData["MessageType"]= AppConsts.ERROR;
        ModelState.AddModelError("ForgotPassword", result.Message);
        return View("ForgotPassword");
    }

    [Authorize]
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await setAccount();
        return View("Index");
    } 

    [Authorize]
    [Route("EditInformation")]
    [HttpGet]
    public async Task<IActionResult> EditInformation()
    {
        await setAccount();
        return View("EditInformation");
    }

    [Authorize]
    [Route("EditInformation")]
    [HttpPost]
    public async Task<IActionResult> EditInformation([FromForm]EditInformationViewModel information)
    {
        ModelState.Clear();
        var validate = _editInformationValidator.Validate(information);
        if(validate.IsValid)
        {
            var result = await _accountRepository.EditInformationAsync(new EditInformationDto{
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Email = information.Email,
                Name = information.Name,
                Picture = information.Picture,
                DefaultPicture = information.DefaultPicture,
            });
            if(result.Success)
                ViewData["MessageType"] = AppConsts.SUCCESS;
            else
                ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("EditInformation", result.Message);
        }   
        else
        {
            ViewData["MessageType"] = AppConsts.WARNING;
            ModelState.AddFluentResult(validate);
        }    
        information.DefaultPicture = false; 
        await setAccount();
        return View("EditInformation", information);
    }

    [Authorize]
    [Route("Settings")]
    [HttpGet]
    public async Task<IActionResult> Settings()
    {
        await setAccount();
        return View("Settings");
    }

    [Authorize]
    [Route("Settings")]
    [HttpPost]
    public async Task<IActionResult> Settings([FromForm]SettingsViewModel settings)
    {
        ModelState.Clear();
        switch(settings.Mode)
        {
            case "ChangePassword":
                var validate = _changePasswordValidator.Validate(settings.ChangePassword);
                if(validate.IsValid)
                {
                    var result = await _accountRepository.ChangePasswordAsync(new ChangePasswordDto{
                        Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Mode = User.FindFirstValue(ClaimTypes.Version),
                        CurrentPassword = settings.ChangePassword.CurrentPassword,
                        NewPassword = settings.ChangePassword.Password,
                    });
                    if(result.Success)
                        ViewData["MessageType"] = AppConsts.SUCCESS;
                    else
                        ViewData["MessageType"] = AppConsts.ERROR;
                    ModelState.AddModelError("ChangePassword", result.Message);
                }
                else
                {
                    ViewData["MessageType"] = AppConsts.WARNING;    
                    ModelState.AddFluentResult(validate);
                }
                break;
            case "DeleteAccount":
                var result2 = await _accountRepository.DeleteAccountAsync(User.FindFirstValue(ClaimTypes.NameIdentifier),settings.DeleteAccount.Confirm);
                if(result2.Success)
                {
                    ViewData["MessageType"] = AppConsts.SUCCESS;
                    return Redirect("/"); 
                }
                else
                    ViewData["MessageType"] = AppConsts.ERROR;
                ModelState.AddModelError("DeleteAccount", result2.Message);
                break;
        }
        await setAccount();
        return View("Settings", settings);
    }
}