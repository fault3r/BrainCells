using System;
using System.Linq.Expressions;
using BrainCells.Application.Common;
using FluentEmail.Core;

namespace BrainCells.Application.Services.SupportEmailService;

public class SupportEmailService : ISupportEmailService
{
    private readonly IFluentEmail _fluentEmail;

    public SupportEmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task<ResultDto> SendMailAsync(string to, string subject, string body)
    {
        try{ 
            var result = await _fluentEmail.To(to)
                .Subject(subject)
                .Body(body)
                .SendAsync();
            return new ResultDto{
                Success = true, 
                Message = "Email sent successfully.",
            };
        }
        catch{
            return new ResultDto{
                Success = false, 
                Message = "Failed. That's all we know!",
            };
        }
    } 
}