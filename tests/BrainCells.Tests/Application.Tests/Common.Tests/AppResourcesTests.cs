using System;
using System.Text;
using BrainCells.Application.Common;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;

namespace BrainCells.Tests.Application.Tests.Common.Tests;

public class AppResourcesTests
{
    [Fact]
    public async Task GetResourceAsync_WhenCalledWithFilename_ReturnsMemoryStream()
    {
        string path = Directory.GetCurrentDirectory();
        Directory.CreateDirectory(Path.Combine(path, "resource"));
        string filename = "test.txt";
        string filepath = Path.Combine(path, "resource", filename);
        string expected = "this is a file for testing purpose.";
        await File.WriteAllTextAsync(filepath, expected);
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(obj => obj.WebRootPath).Returns(path);

        var memory = await AppResources.GetResourceAsync(mockWebHostEnvironment.Object,filename);
        string actual = Encoding.UTF8.GetString(memory.ToArray());

        Assert.Equal(expected, actual);

        File.Delete(filepath);
        Directory.Delete(Path.Combine(path, "resource"));
    }
}