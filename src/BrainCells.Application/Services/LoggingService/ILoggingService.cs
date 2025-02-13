using System;
using static BrainCells.Application.Services.LoggingService.LoggingService;

namespace BrainCells.Application.Services.LoggingService;

public interface ILoggingService
{
    Task LogAccountAsync(string email, LogMode mode);
}