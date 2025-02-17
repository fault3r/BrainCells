using System;
using static BrainCells.Application.Services.LoggingService.LoggingService;

namespace BrainCells.Application.Services.LoggingService;

public interface ILoggingService
{
    enum LogTitle
    {
        SignIn,
        SignUp,
        SignOut,
        ForgotPassword,
        OneTimePassword,
        ChangePassword,
        DeleteAccount,
    }
    
    Task<string> LogAccountAsync(string email, LogTitle title);
}