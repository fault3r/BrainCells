using System;
using BrainCells.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BrainCells.Application.Interfaces;

public interface IDatabaseContext
{
    DbSet<Account> Accounts { get; set; }
    DbSet<Role> Roles { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}
