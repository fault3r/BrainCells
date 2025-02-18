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
using BrainCells.Application.Services.SupportEmailService;
using BrainCells.Application.Services.ResourceMemoryService;
using static BrainCells.Application.Services.LoggingService.ILoggingService;

namespace BrainCells.Application.Services.AccountRepository;

public class AccountRepository : IAccountRepository
{
    private readonly IResourceMemoryService _resourceMemoryService;
    private readonly ILoggingService _loggingService;
    private readonly IDatabaseContext _databaseContext;
    private readonly ISupportEmailService _supportEmailService;
    private readonly IHttpContextAccessor _httpcontextAccessor;

    public AccountRepository(IResourceMemoryService resourceMemoryService, ILoggingService loggingService, IDatabaseContext databaseContext,
        ISupportEmailService supportEmailService, IHttpContextAccessor httpcontextAccessor)
    {
        _resourceMemoryService = resourceMemoryService;
        _loggingService = loggingService;
        _databaseContext = databaseContext;
        _supportEmailService = supportEmailService;
        _httpcontextAccessor = httpcontextAccessor;
    }

    public async Task<ResultDto> SignInAsync(string email, string password, bool persistent)
    {
        try{
            var account = await _databaseContext.Accounts.AsQueryable()
                .Include(e => e.Role)
                .Where(p => p.Email == email.ToLower().Trim())
                .FirstOrDefaultAsync();
            if(account != null)
            {
                bool otpSuccess = false;
                var otp = await _databaseContext.ForgotPasswords.FirstOrDefaultAsync(p => p.AccountId == account.Id);
                if(otp != null)
                    if(otp.OnetimePassword == PasswordHasher.ComputeHash(password))
                    {
                        otpSuccess = true;
                        _databaseContext.ForgotPasswords.Remove(otp);
                        await _databaseContext.SaveChangesAsync();
                        await _loggingService.LogAccountAsync(account.Email, LogTitle.OneTimePassword);

                    }
                if(otpSuccess || PasswordHasher.ComputeHash(password) == account.Password)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.Role, account.Role.Name),
                        new Claim(ClaimTypes.Name, account.Name),
                        new Claim(ClaimTypes.Version, otpSuccess ? "otp" : "normal"),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties {
                        IsPersistent = persistent,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
                    };
                    await _httpcontextAccessor.HttpContext.SignInAsync(principal, properties);
                    await _loggingService.LogAccountAsync(account.Email, LogTitle.SignIn);
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
                Picture = (await _resourceMemoryService.GetResourceAsync(IResourceMemoryService.ProfilePicture)).ToArray(),
            };
            _databaseContext.Accounts.Add(tAccount);
            await _databaseContext.SaveChangesAsync();
            await _loggingService.LogAccountAsync(tAccount.Email, LogTitle.SignUp);
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
            await _loggingService.LogAccountAsync(email, LogTitle.SignOut);
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
        try{
            var account = await _databaseContext.Accounts.AsQueryable()
                .Where(p => p.Email == email.ToLower())
                .FirstOrDefaultAsync();
            if(account != null)
            {
                var old = await _databaseContext.ForgotPasswords.FirstOrDefaultAsync(p => p.AccountId == account.Id);
                if(old != null)
                    _databaseContext.ForgotPasswords.Remove(old);
                string password = OnetimePassword.Create();
                _databaseContext.ForgotPasswords.Add(new ForgotPassword{
                    AccountId = account.Id,
                    OnetimePassword = PasswordHasher.ComputeHash(password),
                });
                await _databaseContext.SaveChangesAsync();
                var result = await _supportEmailService.SendOTPAsync(account.Email, password);
                await _loggingService.LogAccountAsync(account.Email, LogTitle.ForgotPassword);
                if(result.Success)
                    return new ResultDto{
                        Success = true,
                        Message = "Your one-time password has been sent to your email. Use it to verify your identity instead of your current password.",
                    };
                else
                    return result;
            }
            else
                return new ResultDto{
                    Success = false,
                    Message = "No account found. If you think this is a mistake, try resetting your password!",
                };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "Failed to send email. That's all we know!",
            };
        }
    }

    public async Task<ResultDto> ChangePasswordAsync(ChangePasswordDto data)
    {
        try{
            var account = await _databaseContext.Accounts
                .FirstOrDefaultAsync(p => p.Id.ToString() == data.Id);
            if(data.Mode != "otp")
            {
                if(account.Password != PasswordHasher.ComputeHash(data.CurrentPassword))
                {
                    return new ResultDto{
                        Success = false,
                        Message = "The current password you entered is incorrect. Please try again!",
                    };
                }
            }
            account.Password = PasswordHasher.ComputeHash(data.NewPassword);
            _databaseContext.Accounts.Update(account);
            await _databaseContext.SaveChangesAsync();
            await _loggingService.LogAccountAsync(account.Email, LogTitle.ChangePassword);
            return new ResultDto{
                Success = true,
                Message = "Your password has been successfully changed.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "An unexpected error has occurred. That's all we know!",
            }; 
        }
    }

    public async Task<ResultDto> DeleteAccountAsync(string id, string confirm)
    {
        try{
            if(confirm == "delete")
            {
                var account = await _databaseContext.Accounts.FirstOrDefaultAsync(p => p.Id.ToString() == id);
                await SignOutAsync(account.Email);
                _databaseContext.Accounts.Remove(account);
                await _databaseContext.SaveChangesAsync();
                await _loggingService.LogAccountAsync(account.Email, LogTitle.DeleteAccount);
                return new ResultDto{
                    Success = true,
                    Message = "Your account has been successfully deleted. We're sorry to see you go.",
                }; 
            }
            else
                return new ResultDto{
                Success = false,
                Message = "To confirm the deletion of your account, please type 'delete'!",
            }; 
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "An unexpected error has occurred. That's all we know!",
            }; 
        }
    }
}
