using System;
using System.Drawing;

namespace BrainCells.Domain.Entities.Todo;

public class TodoTask
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public byte[] Icon { get; set; }

    public string SetDate { get; set; }

    public PriorityLevel PriorityLevel { get; set; }

    public string DueDate { get; set; }

    public string Reminder { get; set; }

    public string Note { get; set; }

    public bool Completion { get; set; }

    public virtual ICollection<TodoSubTask> SubTasks { get; set; }

    public Guid TodoListId { get; set; }
    public virtual TodoList TodoList { get; set; }
}

 public enum PriorityLevel{
    Low,
    Medium,
    High,
 }

