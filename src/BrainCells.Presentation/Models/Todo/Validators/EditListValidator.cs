using System;
using BrainCells.Presentation.Models.Todo.ViewModels;
using FluentValidation;

namespace BrainCells.Presentation.Models.Todo.Validators;

public class EditListValidator : AbstractValidator<EditListViewModel>
{
    public EditListValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(p => p.Description)
            .NotEmpty()
            .Length(2, 100); 
                 
        RuleFor(p => p.Color)
            .NotEmpty();
    }
}

