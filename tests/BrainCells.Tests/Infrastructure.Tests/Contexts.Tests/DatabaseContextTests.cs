using System;
using BrainCells.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BrainCells.Domain.Entities;
using Moq;
using Xunit;

namespace BrainCells.Tests.Infrastructure.Tests.Contexts.Tests;

public class DatabaseContextTests
{
    private readonly DatabaseContext databaseContext;

    public DatabaseContextTests()
    {
        string connectionString = "Server=localhost,1433; Database=dbfa2; User Id=SA; Password=SQL@server; TrustServerCertificate=true;";
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(obj => obj.GetSection("ConnectionStrings")["Default"])
            .Returns(connectionString);
        var options = new DbContextOptionsBuilder<DatabaseContext>();
        databaseContext = new DatabaseContext(options.Options, mockConfiguration.Object);
    }

    [Fact]
    public void Can_Connect_To_Database()
    {
        //expected: null

        var actual = Record.Exception(() => databaseContext.Roles.ToList());

        Assert.Null(actual);
    }
}