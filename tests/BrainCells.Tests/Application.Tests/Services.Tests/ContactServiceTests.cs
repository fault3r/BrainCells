using System;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.ContactService;
using BrainCells.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BrainCells.Tests.Application.Tests.Services.Tests;

public class ContactServiceTests
{
    private readonly IDatabaseContext _databaseContext;
        private readonly Mock<IDatabaseContext> _mockDatabaseContext;
        private readonly ContactService _contactService;

    public ContactServiceTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("dbTest")
            .Options;
        _databaseContext = new DatabaseContext(options);
    }

    [Fact]
    public async Task SaveMessageAsync_WhenSuccess_SaveMessage()
    {
        var fullname = "John Doe";
        var email = "john.doe@example.com";
        var message = "Hello, this is a test message.";

        var result = await _contactService.SaveMessageAsync(fullname, email, message);

        Assert.True(result.Success);
    }
}