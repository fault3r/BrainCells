using System;

namespace BrainCells.Presentation.Models.Todo.ViewModels;

public class AddTaskViewModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public char Icon { get; set; }

    public string PriorityLevel { get; set; }

    public string DueDate { get; set; }

    public string Reminder { get; set; }

    public string Note { get; set; }
    
}