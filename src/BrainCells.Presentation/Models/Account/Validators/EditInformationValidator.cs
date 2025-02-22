using System;
using System.Data;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;

namespace BrainCells.Presentation.Models.Account.Validators;

public class EditInformationValidator : AbstractValidator<EditInformationViewModel>
{
    public EditInformationValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(p => p.Name)
            .NotEmpty()
            .Length(2,30);
        
        RuleFor(p => p)
            .Must(p => p.Picture.FileName.Contains("jpg"))
            .When(p => p.Picture != null)
            .WithMessage("'Picture' Please choose a jpg file!");
            
    }
}
