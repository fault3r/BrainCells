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
            ResultDto result = new();
            var response = await _fluentEmail.To(to)
                .Subject(subject)
                .Body(body)
                .SendAsync();
            if(response.Successful)
            {
                result.Success = true;
                result.Message = "The email has been sent successfully.";
            }
            else
            {
                result.Success = false;
                result.Message = "Failed to send email. That's all we know!";
            }
            return result;
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "Failed to send email. That's all we know!",
            };
        }
    } 
}