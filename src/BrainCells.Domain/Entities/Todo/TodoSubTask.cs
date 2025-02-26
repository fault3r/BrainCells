using System;

namespace BrainCells.Domain.Entities.Todo;

public class TodoSubTask
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; }

    public bool Completion { get; set; }

    public Guid TodoTaskId { get; set; }
    public virtual TodoTask TodoTask { get; set; }
}
