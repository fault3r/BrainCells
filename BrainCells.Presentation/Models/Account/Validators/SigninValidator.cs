using System;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;

namespace BrainCells.Presentation.Models.Account.Validators;

public class SigninValidator : AbstractValidator<SigninViewModel>
{
    public SigninValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(p => p.Password)
            .NotEmpty()
            .Length(8,30);
    }
}