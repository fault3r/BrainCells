using System;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.Common;
using BrainCells.Domain.Entities.Accounts;

namespace BrainCells.Application.Services.AccountRepository;

public class AccountRepository : IAccountRepository
{

    private readonly IDatabaseContext _databaseContext;

    public AccountRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public RepositoryResultDto SignUp(SignUpDto account)
    {
        var tAccount = new Account {
            Email=account.Email,
            Password=account.Password,
            Name=account.Name,
            RoleId=Guid.Parse("9ab668fb-6010-4786-9b0f-e77432580acb"),
        };
        _databaseContext.Accounts.Add(tAccount);
        _databaseContext.SaveChanges();
        return new RepositoryResultDto {Success=true, Message="Done."};
    }

}
