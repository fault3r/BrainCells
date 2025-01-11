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

    public async Task<RepositoryResultDto> SignIn(string email, string password, bool persistent)
    {
        var account = _databaseContext.Accounts.AsQueryable()
            .Include(e => e.Role)
            .Where(p => p.Email == email.ToLower().Trim() && p.Password  == PasswordHasher.ComputeHash(password))
            .FirstOrDefault();
        if(account != null)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.Name),
                new Claim(ClaimTypes.Name, account.Name),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties(){IsPersistent=persistent};
            await _httpcontextAccessor.HttpContext.SignInAsync(principal, properties);
            return new RepositoryResultDto{Success=true, Message="Done."};
        }
        else
            return new RepositoryResultDto{Success=false, Message="Email address or password is incorrect!"};
    }

    public RepositoryResultDto SignUp(SignUpDto account)
    {
        var tAccount = new Account {
            Email=account.Email,
            Password=account.Password,
            Name=account.Name,
            RoleId=Guid.Parse("66b42c36-dccf-4e55-b03e-09d74867f336"),
        };
        _databaseContext.Accounts.Add(tAccount);
        _databaseContext.SaveChanges();
        return new RepositoryResultDto {Success=true, Message="Done."};
    }

}
