using System;
using System.Drawing;
using BrainCells.Domain.Entities.Accounts;

namespace BrainCells.Domain.Entities.Todo;

public class TodoList
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public byte[] Picture { get; set; }

    public virtual ICollection<TodoTask> Tasks { get; set; }

    public Guid AccountId { get; set;} 
    public virtual Account Account{ get; set; }
}
