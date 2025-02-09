using System;
using BrainCells.Application.Common;

namespace BrainCells.Application.Services.SupportEmailService;

public interface ISupportEmailService
{
    Task<ResultDto> SendOTPAsync(string to, string otp);
}