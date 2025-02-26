using System;

namespace BrainCells.Application.Services.LoggingService;

public interface ILoggingService
{   
    Task<string> LogAccountAsync(string email, LogTitle title);
}