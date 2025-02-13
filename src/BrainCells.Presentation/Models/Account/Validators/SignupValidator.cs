using System;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;

namespace BrainCells.Presentation.Models.Account.Validators;

public class SignupValidator : AbstractValidator<SignupViewModel>
{
    public SignupValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(p => p.Password)
            .NotEmpty()
            .Length(8,30);

        RuleFor(p => p.ConfirmPassword)
            .NotEmpty()
            .Length(8,30);

        RuleFor(p => p.Name)
            .NotEmpty()
            .Length(2,30);    

        RuleFor(p => p)
            .Must(p => p.Password == p.ConfirmPassword)
            .WithMessage("Password and confirm is not match.");
        
        RuleFor(p => p)
            .Must(p => p.Agree == true)
            .WithMessage("Agree with our terms.");
    }
}