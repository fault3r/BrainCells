using System;
using static BrainCells.Application.Services.LoggingService.LoggingService;

namespace BrainCells.Application.Services.LoggingService;

public interface ILoggingService
{
    Task<string> LogAccountAsync(string email, LogTitle title);
}