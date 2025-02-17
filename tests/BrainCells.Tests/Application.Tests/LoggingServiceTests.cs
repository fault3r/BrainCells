using System;
using System.Threading.Tasks;
using BrainCells.Application.Services.LoggingService;
using Xunit;
using static BrainCells.Application.Services.LoggingService.ILoggingService;

namespace BrainCells.Tests.Application.Tests;

public class LoggingServiceTests
{
    [Fact]
    public async Task LogAccountAsync_Can_Write_Log()
    {//System Test
        //Arrange
        string email = "system@test";
        LogTitle logTitle = LogTitle.SignIn;
        string root = "logRoot";
        var loggingService = new LoggingService(root);
        
        //Act
        var filePath = await loggingService.LogAccountAsync(email, logTitle);
        bool fileExist = File.Exists(filePath);

        //Assert
        Assert.NotEqual<string>("error", filePath);
        Assert.True(fileExist);

        //Cleanup
        Directory.Delete(root, true);
    }
}