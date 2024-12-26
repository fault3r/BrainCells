using System;

namespace BrainCells.Domain.Entities.Accounts;

public class Account
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Guid RoleId { get; set; }
    public virtual Role Role{ get; set; }

    public string Name { get; set; }
}
