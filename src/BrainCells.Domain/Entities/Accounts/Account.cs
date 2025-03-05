using System;
using BrainCells.Domain.Entities.Todo;

namespace BrainCells.Domain.Entities.Accounts;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Email { get; set; }

    public string Password { get; set; }

    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }

    public string Name { get; set; }

    public byte[] Picture { get; set; }

    public virtual ForgotPassword ForgotPassword { get; set; }

    public virtual ICollection<TodoList> TodoLists { get; set; }
}
