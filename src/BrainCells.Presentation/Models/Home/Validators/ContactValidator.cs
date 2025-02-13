using System;
using BrainCells.Presentation.Models.Home.ViewModels;
using FluentValidation;

namespace BrainCells.Presentation.Models.Home.Validators;

public class ContactValidator : AbstractValidator<ContactViewModel>
{
    public ContactValidator()
    {
        RuleFor(p => p.FullName)
            .NotEmpty()
            .Length(3,50);
        
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(p => p.Message)
            .NotEmpty()
            .Length(5,500);
    }
}