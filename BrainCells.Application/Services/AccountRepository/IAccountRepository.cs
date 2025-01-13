using System;
using BrainCells.Application.Services.Common;

namespace BrainCells.Application.Services.AccountRepository;

public interface IAccountRepository
{
    Task<RepositoryResultDto> SignIn(string email, string password, bool persistent);
    
    Task<RepositoryResultDto> SignUp(SignUpDto account);

}
