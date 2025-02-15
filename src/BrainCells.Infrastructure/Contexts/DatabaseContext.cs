using System;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Domain.Entities;
using BrainCells.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BrainCells.Infrastructure.Contexts;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions options) : base(options){}

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ForgotPassword> ForgotPasswords { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Account>().HasKey(p => p.Id);
        builder.Entity<Account>().HasIndex(p => p.Email).IsUnique();
        builder.Entity<Account>().HasOne(e => e.Role).WithMany(e => e.Accounts)
            .HasForeignKey(p => p.RoleId);
        builder.Entity<Account>().HasOne(e => e.ForgotPassword).WithOne(e => e.Account)
            .HasForeignKey<ForgotPassword>(p => p.AccountId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Role>().HasKey(p => p.Id);
        builder.Entity<Role>().HasData(new Role {
            Id = Guid.Parse(AppConsts.ADMIN),
            Name = "ADMIN",
        }, new Role{
            Id = Guid.Parse(AppConsts.ACCOUNT),
            Name = "ACCOUNT",
        });

        builder.Entity<ForgotPassword>().HasKey(p => p.AccountId);

        builder.Entity<Contact>().HasKey(p => p.Id);
    }
}
