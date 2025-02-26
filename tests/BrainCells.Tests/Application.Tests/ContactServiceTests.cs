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
    public async Task SaveMessageAsync_Can_Write_To_Database()
    {//System Test
        //Arrange
        string connectionString = CommonTests.ConnectionString;
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlServer(connectionString)
            .Options;
        var databaseContext = new DatabaseContext(options);
        var contactService = new ContactService(databaseContext);
        string input = "SystemTest";

        //Act
        var result = await contactService.SaveMessageAsync(input, input, input);
        var contact = await databaseContext.Contacts.FirstOrDefaultAsync(p => p.Email == input.ToLower());

        //Assert
        Assert.True(result.Success);
        Assert.NotNull(contact);

        //Cleanup
        databaseContext.Contacts.Remove(contact);
        await databaseContext.SaveChangesAsync();
    }

    [Fact]
    public async Task SaveMessageAsync_WhenDatabaseConnectionOkay_SaveMessage()
    {//Unit Test
        //Arrange
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("dbTest")
            .Options;
        var databaseContext = new DatabaseContext(options);
        var contactService = new ContactService(databaseContext);
        string input = "UnitTest";

        //Act
        var result = await contactService.SaveMessageAsync(input, input, input);

        //Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task SaveMessageAsync_WhenDatabaseConnectionError_ReturnFailure()
    {//Unit Test
        //Arrange
        var mockDatabaseContext = new Mock<IDatabaseContext>();
        mockDatabaseContext.Setup(m => m.SaveChangesAsync(default))
            .ThrowsAsync(new Exception());
        var contactService = new ContactService(mockDatabaseContext.Object);
        string input = "UnitTest";

        //Act
        var result = await contactService.SaveMessageAsync(input, input, input);

        //Assert
        Assert.False(result.Success);
    }
}