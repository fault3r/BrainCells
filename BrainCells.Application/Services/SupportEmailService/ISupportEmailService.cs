using System;
using BrainCells.Application.Common;

namespace BrainCells.Application.Services.SupportEmailService;

public interface ISupportEmailService
{
    Task<ResultDto> SendMailAsync(string to, string subject, string body);
}