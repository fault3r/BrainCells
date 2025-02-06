using System;

namespace BrainCells.Presentation.Models.Account.ViewModels;

public class SettingsViewModel
{
    public string Mode { get; set; }
    
    public ChangePasswordViewModel ChangePassword { get; set; }
}