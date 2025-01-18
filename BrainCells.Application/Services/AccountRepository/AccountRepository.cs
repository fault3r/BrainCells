using System;
using System.Security.Claims;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.Common;
using BrainCells.Domain.Entities.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace BrainCells.Application.Services.AccountRepository;

public class AccountRepository : IAccountRepository
{

    private readonly IDatabaseContext _databaseContext;
    private readonly IHttpContextAccessor _httpcontextAccessor;

    public AccountRepository(IDatabaseContext databaseContext, IHttpContextAccessor httpcontextAccessor)
    {
        _databaseContext = databaseContext;
        _httpcontextAccessor = httpcontextAccessor;
    }

    public async Task<RepositoryResultDto> SignInAsync(string email, string password, bool persistent)
    {
        try{
            var account = _databaseContext.Accounts.AsQueryable()
                .Include(e => e.Role)
                .Where(p => p.Email == email.ToLower().Trim() && p.Password  == PasswordHasher.ComputeHash(password))
                .FirstOrDefault();
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
                var properties = new AuthenticationProperties {IsPersistent = persistent};
                await _httpcontextAccessor.HttpContext.SignInAsync(principal, properties);
                return new RepositoryResultDto {
                    Success = true,
                    Message = $"Welcome back, {account.Name}! You have successfully logged in.",
                };
            }
            else
                return new RepositoryResultDto {
                    Success = false,
                    Message = "Login failed. Please check your email and password and try again!",
                };
        }
        catch{
                return new RepositoryResultDto{   
                    Success = false,
                    Message = "An unexpected error has occurred. That's all we know!",
                };
        }
    }

    public async Task<RepositoryResultDto> SignUpAsync(SignUpDto account)
    {
        try{
            var tAccount = new Account {
                Email = account.Email.ToLower(),
                Password = PasswordHasher.ComputeHash(account.Password),
                Name = account.Name.Trim(),
                RoleId = Guid.Parse(AppConsts.ACCOUNT),
            };
            _databaseContext.Accounts.Add(tAccount);
            await _databaseContext.SaveChangesAsync();
            return new RepositoryResultDto {
                Success = true,
                Message = $"Congratulations, {tAccount.Name}! Your account has been created successfully. You can now log in.",
            };
        }
        catch(Exception ex){
            if(ex.InnerException != null)
            {
                if(ex.InnerException.Message.Contains("duplicate key"))
                    return new RepositoryResultDto {
                        Success = false,
                        Message = "This email address is already associated with an account. Please log in or use a different email.",
                    };
            }
            return new RepositoryResultDto {
                Success = false,
                Message = "An unexpected error has occurred. That's all we know!",
            };  
        }
    }

    public async Task<RepositoryResultDto> SignOutAsync()
    {
        try{
            await _httpcontextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new RepositoryResultDto {
                Success = true,
                Message = "You have successfully logged out. We hope to see you again soon.",
            };
        }
        catch{
            return new RepositoryResultDto {
                Success = false,
                Message = "An unexpected error has occurred. That's all we know!",
            }; 
       }
    }
}
