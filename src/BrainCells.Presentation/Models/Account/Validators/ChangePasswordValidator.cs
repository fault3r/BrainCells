using System;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;

namespace BrainCells.Presentation.Models.Account.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
{
    public ChangePasswordValidator()
    {
        RuleFor(p => p.CurrentPassword)
            .NotEmpty()
            .Length(8,30);

        RuleFor(p => p.Password)
            .NotEmpty()
            .Length(8,30);

        RuleFor(p => p.ConfirmPassword)
            .NotEmpty()
            .Length(8,30);

        RuleFor(p => p)
            .Must(p => p.Password == p.ConfirmPassword)
            .WithMessage("Password and confirm is not match.");
    }
}