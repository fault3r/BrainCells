using System;

namespace BrainCells.Presentation.Models.Account.ViewModels;

public class SignupViewModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string Name { get; set; }

    public bool Agree { get; set; }
}