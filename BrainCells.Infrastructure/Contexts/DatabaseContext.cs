using System;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BrainCells.Infrastructure.Contexts;

public class DatabaseContext : DbContext,IDatabaseContext
{
    public DatabaseContext(DbContextOptions options) :base(options){}

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Account>().HasKey(p => p.Id);
        builder.Entity<Account>().HasOne(e => e.Role).WithMany(e => e.Accounts)
            .HasForeignKey(p => p.RoleId);

        builder.Entity<Role>().HasKey(p => p.Id);
        builder.Entity<Role>().HasData(new Role {
            Id = Guid.Parse(AppRoles.ADMIN),
            Name = "ADMIN",
        }, new Role{
            Id = Guid.Parse(AppRoles.ACCOUNT),
            Name = "ACCOUNT",
        });        
    }
}
