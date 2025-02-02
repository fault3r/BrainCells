using System;
using BrainCells.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BrainCells.Application.Interfaces;

public interface IDatabaseContext
{
    DbSet<Account> Accounts { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<ForgotPassword> ForgotPasswords { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}
