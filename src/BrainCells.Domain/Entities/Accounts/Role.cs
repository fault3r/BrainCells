using System;

namespace BrainCells.Domain.Entities.Accounts;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }
    
}
