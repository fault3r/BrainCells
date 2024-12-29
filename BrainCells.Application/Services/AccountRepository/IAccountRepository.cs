using System;
using BrainCells.Application.Services.Common;

namespace BrainCells.Application.Services.AccountRepository;

public interface IAccountRepository
{
    RepositoryResultDto SignUp(SignUpDto account);

}
