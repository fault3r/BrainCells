using System;
using BrainCells.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BrainCells.Domain.Entities;
using Moq;
using Xunit;
using BrainCells.Domain.Entities.Accounts;

namespace BrainCells.Tests.Infrastructure.Tests.Contexts.Tests;

public class DatabaseContextTests
{
    private readonly DatabaseContext databaseContext;

    public DatabaseContextTests()
    {
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(obj => obj.GetSection("ConnectionStrings")["Default"])
            .Returns("Server=localhost,1433; Database=dbfa2; User Id=SA; Password=SQL@server; TrustServerCertificate=true;");
        var options = new DbContextOptionsBuilder<DatabaseContext>();
        databaseContext = new DatabaseContext(options.Options, mockConfiguration.Object);
    }

    [Fact]
    public async Task Can_Connect_To_Database()
    {
        var expected = await Record.ExceptionAsync(async () => await databaseContext.Roles.ToListAsync());

        Assert.Null(expected);
    }

    [Fact]
    public async Task Can_Save_Changes()
    {
        var testData = new Role{
            Id = Guid.Parse("00000000-fa00-0000-0000-000000000000"),
            Name = "Test",
        };

        databaseContext.Roles.Add(testData);
        var expected = await Record.ExceptionAsync(async () => await databaseContext.SaveChangesAsync());

        Assert.Null(expected);

        databaseContext.Roles.Remove(testData);
        await databaseContext.SaveChangesAsync();
    }
}