using System;

namespace BrainCells.Application.Services.TodoRepository;

public class AddTaskDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public char Icon { get; set; }

    public string PriorityLevel { get; set; }

    public string DueDate { get; set; }

    public string Reminder { get; set; }

    public string Note { get; set; }

}