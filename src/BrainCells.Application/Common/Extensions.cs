using System;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrainCells.Application.Common;

public static class Extensions
{
    public static ModelStateDictionary AddFluentResult(this ModelStateDictionary source, ValidationResult fluent)
    {
        foreach(var error in fluent.Errors)
            source.AddModelError(error.PropertyName, error.ErrorMessage);
        return source;
    }
}