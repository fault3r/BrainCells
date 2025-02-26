using System;
using System.Linq.Expressions;
using System.Text;
using BrainCells.Application.Common;
using BrainCells.Application.Services.ResourceMemoryService;
using FluentEmail.Core;
using Microsoft.AspNetCore.Hosting;

namespace BrainCells.Application.Services.SupportEmailService;

public class SupportEmailService : ISupportEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly IResourceMemoryService _resourceMemoryService;

    public SupportEmailService(IFluentEmail fluentEmail, IResourceMemoryService resourceMemoryService)
    {
        _fluentEmail = fluentEmail;
        _resourceMemoryService = resourceMemoryService;
    }

    public async Task<ResultDto> SendOTPAsync(string to, string otp)
    {
        try{ 
            var resource = await _resourceMemoryService.GetResourceAsync(ResourceMemoryItems.OtpTemplate);
            string template = Encoding.UTF8.GetString(resource.ToArray());
            string body = template.Replace("[OneTimePassword]", otp);
            var response = await _fluentEmail
                .To(to)
                .Subject("One-Time Password")
                .Body(body, true)
                .SendAsync();
            if(response.Successful)
            {
                return new ResultDto{
                    Success = true,
                    Message = "The one-time password has been sent successfully.",
                };
            }
            else
                return new ResultDto{
                    Success = false,
                    Message = "Failed to send one-time password. That's all we know!",
                };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "Failed to send email. That's all we know!",
            };
        }
    } 
}