using System;
using System.Threading.Tasks;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.ContactService;
using BrainCells.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BrainCells.Tests.Application.Tests;

public class ContactServiceTests
{
    [Fact]
    public async Task SaveMessageAsync_WhenConnectionOkay_ReturnsSuccess()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("dbTest")
            .Options;
        var databaseContext = new DatabaseContext(options);
        var contactService = new ContactService(databaseContext);
        string fullname = "xUnit Test";
        string email = "test@unit.x";
        string message = "this is a test message.";

        var result = await contactService.SaveMessageAsync(fullname, email, message);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task SaveMessageAsync_WhenThrownException_ReturnFailure()
    {
        var mockDatabaseContext = new Mock<IDatabaseContext>();
        mockDatabaseContext.Setup(m => m.SaveChangesAsync(default)).ThrowsAsync(new Exception());
        var contactService = new ContactService(mockDatabaseContext.Object);
        string fullname = "xUnit Test";
        string email = "test@unit.x";
        string message = "this is a test message.";

        var result = await contactService.SaveMessageAsync(fullname, email, message);

        Assert.False(result.Success);
    }
}