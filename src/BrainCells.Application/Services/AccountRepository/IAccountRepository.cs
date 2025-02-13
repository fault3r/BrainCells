using System;
using BrainCells.Application.Common;

namespace BrainCells.Application.Services.AccountRepository;

public interface IAccountRepository
{
    Task<ResultDto> SignInAsync(string email, string password, bool persistent);
    
    Task<ResultDto> SignUpAsync(SignUpDto account);

    Task<ResultDto> SignOutAsync(string email);

    Task<AccountDto?> ViewAccountAsync(string id);

    Task<ResultDto> ForgotPasswordAsync(string email);

    Task<ResultDto> ChangePasswordAsync(ChangePasswordDto data);

    Task<ResultDto> DeleteAccountAsync(string id, string confirm);
}
