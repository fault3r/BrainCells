using System;
using BrainCells.Application.Services.Common;

namespace BrainCells.Application.Services.AccountRepository;

public interface IAccountRepository
{
    Task<RepositoryResultDto> SignInAsync(string email, string password, bool persistent);
    
    Task<RepositoryResultDto> SignUpAsync(SignUpDto account);

    Task<RepositoryResultDto> SignOutAsync();
}
