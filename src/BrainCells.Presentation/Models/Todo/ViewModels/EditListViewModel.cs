using System;

namespace BrainCells.Presentation.Models.Todo.ViewModels;

public class EditListViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public string CurrentPicture { get; set; }

    public IFormFile Picture { get; set; }

    public bool DefaultPicture { get; set; } = false;
}
