using System;

namespace BrainCells.Presentation.Models.Account.ViewModels;

public class EditInformationViewModel
{
    public string Email { get; set; }

    public string Name { get; set; }

    public IFormFile Picture { get; set; }
}
