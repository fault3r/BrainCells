using System;

namespace BrainCells.Presentation.Models.Account.ViewModels;

public class SigninViewModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool Persistent { get; set; }
}