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
    public async Task SaveMessageAsync_WhenDatabaseConnectionOkay_SaveMessage()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("dbTest")
            .Options;
        var databaseContext = new DatabaseContext(options);
        var contactService = new ContactService(databaseContext);
        string input = "xUnitTest";

        var result = await contactService.SaveMessageAsync(input, input, input);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task SaveMessageAsync_WhenDatabaseConnectionError_ReturnFailure()
    {
        var mockDatabaseContext = new Mock<IDatabaseContext>();
        mockDatabaseContext.Setup(m => m.SaveChangesAsync(default))
            .ThrowsAsync(new Exception());
        var contactService = new ContactService(mockDatabaseContext.Object);
        string input = "xUnitTest";

        var result = await contactService.SaveMessageAsync(input, input, input);

        Assert.False(result.Success);
    }
}