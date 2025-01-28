using System;
using FluentEmail.Core;

namespace BrainCells.Application.Services.SupportEmailService;

public class SupportEmailService
{
    private readonly IFluentEmail _fluentEmail;

    public SupportEmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

     
    
}