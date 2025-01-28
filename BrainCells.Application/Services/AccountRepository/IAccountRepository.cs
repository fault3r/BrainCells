using System;
using BrainCells.Application.Common;

namespace BrainCells.Application.Services.AccountRepository;

public interface IAccountRepository
{
    Task<ResultDto> SignInAsync(string email, string password, bool persistent);
    
    Task<ResultDto> SignUpAsync(SignUpDto account);

    Task<ResultDto> SignOutAsync();

    Task<AccountDto> ViewAccountAsync(string id);
}
