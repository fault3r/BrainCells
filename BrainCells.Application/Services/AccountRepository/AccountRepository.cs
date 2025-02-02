using System;
using System.Security.Claims;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Domain.Entities.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using BrainCells.Application.Services.LoggingService;
using static BrainCells.Application.Services.LoggingService.LoggingService;
using BrainCells.Application.Services.SupportEmailService;

namespace BrainCells.Application.Services.AccountRepository;

public class AccountRepository : IAccountRepository
{
    private readonly ILoggingService _loggingService;
    private readonly IDatabaseContext _databaseContext;
    private readonly ISupportEmailService _supportEmailService;
    private readonly IHttpContextAccessor _httpcontextAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AccountRepository(ILoggingService loggingService, IDatabaseContext databaseContext,
        ISupportEmailService supportEmailService, IHttpContextAccessor httpcontextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        _loggingService = loggingService;
        _databaseContext = databaseContext;
        _supportEmailService = supportEmailService;
        _httpcontextAccessor = httpcontextAccessor;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<ResultDto> SignInAsync(string email, string password, bool persistent)
    {
        try{
            var account = await _databaseContext.Accounts.AsQueryable()
                .Include(e => e.Role)
                .Where(p => p.Email == email.ToLower().Trim() && p.Password  == PasswordHasher.ComputeHash(password))
                .FirstOrDefaultAsync();
            if(account != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, account.Role.Name),
                    new Claim(ClaimTypes.Name, account.Name),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties {
                    IsPersistent = persistent,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
                };
                await _httpcontextAccessor.HttpContext.SignInAsync(principal, properties);
                await _loggingService.LogAccountAsync(account.Email, LogMode.SignIn);
                return new ResultDto{
                    Success = true,
                    Message = $"Welcome back, {account.Name}! You have successfully logged in.",
                };
            }
            else
                return new ResultDto{
                    Success = false,
                    Message = "Login failed. Please check your email and password and try again!",
                };
        }
        catch{
                return new ResultDto{   
                    Success = false,
                    Message = "An unexpected error has occurred. That's all we know!",
                };
        }
    }

    public async Task<ResultDto> SignUpAsync(SignUpDto account)
    {
        try{
            var tAccount = new Account{
                Email = account.Email.ToLower(),
                Password = PasswordHasher.ComputeHash(account.Password),
                RoleId = Guid.Parse(AppConsts.ACCOUNT),
                Name = account.Name.Trim(),
                Picture = AppResources.GetResource(_webHostEnvironment, AppResources.ProfilePicture).ToArray(),
            };
            _databaseContext.Accounts.Add(tAccount);
            await _databaseContext.SaveChangesAsync();
            await _loggingService.LogAccountAsync(tAccount.Email, LogMode.SignUp);
            return new ResultDto{
                Success = true,
                Message = $"Congratulations, {tAccount.Name}! Your account has been created successfully. You can now log in.",
            };
        }
        catch(Exception ex){
            if(ex.InnerException != null)
            {
                if(ex.InnerException.Message.Contains("duplicate key"))
                    return new ResultDto{
                        Success = false,
                        Message = "This email address is already associated with an account. Please log in or use a different email.",
                    };
            }
            return new ResultDto{
                Success = false,
                Message = "An unexpected error has occurred. That's all we know!",
            };  
        }
    }

    public async Task<ResultDto> SignOutAsync(string email)
    {
        try{
            await _httpcontextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _loggingService.LogAccountAsync(email, LogMode.SignOut);
            return new ResultDto{
                Success = true,
                Message = "You have successfully logged out. We hope to see you again soon.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "An unexpected error has occurred. That's all we know!",
            }; 
       }
    }

    public async Task<AccountDto?> ViewAccountAsync(string id)
    {
        try{
            var account = await _databaseContext.Accounts.AsQueryable()
                .Include(e => e.Role)
                .Where(p => p.Id.ToString() == id)
                .FirstOrDefaultAsync();
            if(account == null)
                return null;
            return new AccountDto{
                Id = account.Id.ToString(),
                Email = account.Email,
                Role = account.Role.Name,
                Name = account.Name,
                Picture = Convert.ToBase64String(account.Picture),
            };
        }
        catch{
            return null;
        }
    }

    public async Task<ResultDto> ForgotPasswordAsync(string email)
    {
        var account = await _databaseContext.Accounts.AsQueryable()
            .Where(p => p.Email == email.ToLower())
            .FirstOrDefaultAsync();
        if(account != null)
        {
            var old = await _databaseContext.ForgotPasswords.AsQueryable()
                .Where(p => p.AccountId == account.Id)
                .FirstOrDefaultAsync();
            if(old != null)
                _databaseContext.ForgotPasswords.Remove(old);

            //make a code
            _databaseContext.ForgotPasswords.Add(new ForgotPassword{
                AccountId = account.Id,
                VerificationCode = "123456",
            });

            await _databaseContext.SaveChangesAsync();

            await _supportEmailService.SendMailAsync("hamed.damaavandi@gmail.com","verification code","123456");

            return new ResultDto{
                Success = true,
                Message = "The verification code has send.",
            };
        }
        else
            return new ResultDto{
                Success = false,
                Message = "Account not found!",
            };
    }

}
