using System;
using System.Linq.Expressions;
using System.Text;
using BrainCells.Application.Common;
using FluentEmail.Core;
using Microsoft.AspNetCore.Hosting;

namespace BrainCells.Application.Services.SupportEmailService;

public class SupportEmailService : ISupportEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SupportEmailService(IFluentEmail fluentEmail, IWebHostEnvironment webHostEnvironment)
    {
        _fluentEmail = fluentEmail;
        _webHostEnvironment = webHostEnvironment;
    }

    private string useTemplate(string data)
    {
        var templateFile = AppResources.GetResource(_webHostEnvironment, AppResources.EmailTemplate);
        string template  = Encoding.UTF8.GetString(templateFile.ToArray());
        return template.Replace("[OTP-PWD]", data);
    } 

    public async Task<ResultDto> SendMailAsync(string to, string subject, string body)
    {
        try{ 
            ResultDto result = new();
            body = useTemplate(body);
            var response = await _fluentEmail.To(to)
                .Subject(subject)
                .Body(body, true)
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