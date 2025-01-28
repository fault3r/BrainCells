using System;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BrainCells.Infrastructure.Contexts;

public class DatabaseContext : DbContext, IDatabaseContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(DbContextOptions options, IConfiguration configuration) :base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer(_configuration.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Account>().HasKey(p => p.Id);
        builder.Entity<Account>().HasOne(e => e.Role).WithMany(e => e.Accounts)
            .HasForeignKey(p => p.RoleId);
        builder.Entity<Account>().HasIndex(p => p.Email).IsUnique();

        builder.Entity<Role>().HasKey(p => p.Id);
        builder.Entity<Role>().HasData(new Role {
            Id = Guid.Parse(AppConsts.ADMIN),
            Name = "ADMIN",
        }, new Role{
            Id = Guid.Parse(AppConsts.ACCOUNT),
            Name = "ACCOUNT",
        });        
    }
}
