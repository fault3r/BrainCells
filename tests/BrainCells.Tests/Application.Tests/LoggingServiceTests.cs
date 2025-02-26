using System;
using BrainCells.Application.Services.LoggingService;
using Xunit;

namespace BrainCells.Tests.Application.Tests;

public class LoggingServiceTests
{
    [Fact]
    public async Task LogAccountAsync_Can_Write_Log()
    {//System Test
        //Arrange
        string rootPath = "logRoot";
        var loggingService = new LoggingService(rootPath);
        LogTitle logTitle = LogTitle.SignIn;
        string email = "system@test";
        
        //Act
        var filePath = await loggingService.LogAccountAsync(email, logTitle);
        bool fileExist = File.Exists(filePath);

        //Assert
        Assert.NotEqual<string>("error", filePath);
        Assert.True(fileExist);

        //Cleanup
        Directory.Delete(rootPath, true);
    }
}