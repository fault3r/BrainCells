using System;
using System.Data;
using BrainCells.Presentation.Models.Todo.ViewModels;
using FluentValidation;

namespace BrainCells.Presentation.Models.Todo.Validators;

public class AddListValidator : AbstractValidator<AddListViewModel>
{
    public AddListValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .Length(2, 50);

        RuleFor(p => p.Description)
            .NotEmpty()
            .Length(2, 100); 
    }
}
