using System;
using System.Threading.Tasks;
using BrainCells.Application.Services.LoggingService;
using Moq;
using Xunit;
using static BrainCells.Application.Services.LoggingService.LoggingService;

namespace BrainCells.Tests.Application.Tests;

public class LoggingServiceTests
{
    [Fact]
    public async Task LogAccountAsync_Can_Write_Log()
    {
        //System Test
        //Arrange
        string email = "test@x.x";
        LogTitle logTitle = LogTitle.SignIn;
        string path = "testRoot";
        var loggingService = new LoggingService(path);
        
        //Act
        var filePath = await loggingService.LogAccountAsync(email, logTitle);
        bool fileExist = File.Exists(filePath);

        //Assert
        Assert.NotEqual<string>("error", filePath);
        Assert.True(fileExist);

        //Cleanup
        Directory.Delete(path, true);
    }
}