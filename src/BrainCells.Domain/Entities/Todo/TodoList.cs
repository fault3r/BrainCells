using System;
using System.Drawing;

namespace BrainCells.Domain.Entities.Todo;

public class TodoList
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public byte[] Picture { get; set; }

    public virtual ICollection<TodoTask> Tasks { get; set; }

}
