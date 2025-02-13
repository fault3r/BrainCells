using System;

namespace BrainCells.Presentation.Models.Account.ViewModels;

public class ChangePasswordViewModel
{
    public string CurrentPassword { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}
