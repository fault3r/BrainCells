using System;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Domain.Entities;
using BrainCells.Domain.Entities.Accounts;
using BrainCells.Domain.Entities.Todo;
using Microsoft.EntityFrameworkCore;

namespace BrainCells.Infrastructure.Contexts;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions options) : base(options){}

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ForgotPassword> ForgotPasswords { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }
    public DbSet<TodoSubTask> TodoSubTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Account>().HasKey(p => p.Id);
        builder.Entity<Account>().HasIndex(p => p.Email).IsUnique();
        builder.Entity<Account>().HasOne(e => e.Role).WithMany(e => e.Accounts)
            .HasForeignKey(p => p.RoleId);
        builder.Entity<Account>().HasOne(e => e.ForgotPassword).WithOne(e => e.Account)
            .HasForeignKey<ForgotPassword>(p => p.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Account>().HasMany(e => e.TodoLists).WithOne(e => e.Account)
            .HasForeignKey(p => p.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Role>().HasKey(p => p.Id);
        builder.Entity<Role>().HasMany(e => e.Accounts).WithOne(e => e.Role)
            .HasForeignKey(p => p.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Role>().HasData(new Role {
            Id = Guid.Parse(AppConsts.ADMIN),
            Name = "ADMIN",
        }, new Role{
            Id = Guid.Parse(AppConsts.ACCOUNT),
            Name = "ACCOUNT",
        });

        builder.Entity<ForgotPassword>().HasKey(p => p.AccountId);

        builder.Entity<Contact>().HasKey(p => p.Id);

        builder.Entity<TodoList>().HasKey(p => p.Id);
        builder.Entity<TodoList>().HasMany(e => e.Tasks).WithOne(e => e.TodoList)
            .HasForeignKey(p => p.TodoListId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<TodoList>().HasOne(e => e.Account).WithMany(e => e.TodoLists)
            .HasForeignKey(p => p.AccountId);

        builder.Entity<TodoTask>().HasKey(p => p.Id);
        builder.Entity<TodoTask>().HasOne(e => e.TodoList).WithMany(e => e.Tasks)
            .HasForeignKey(p => p.TodoListId);

        builder.Entity<TodoSubTask>().HasKey(p => p.Id);
        builder.Entity<TodoSubTask>().HasOne(e => e.TodoTask).WithMany(e => e.SubTasks)
            .HasForeignKey(p => p.TodoTaskId);
    }
}
