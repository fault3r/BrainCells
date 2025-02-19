using System;
using System.Text;
using System.Threading.Tasks;
using BrainCells.Application.Services.ResourceMemoryService;
using Xunit;

namespace BrainCells.Tests.Application.Tests;

public class ResourceMemoryTests
{
    [Fact]
    public async Task GetResourceAsync_Can_Get_Resource()
    {//System Test
        //Arrange
        string rootPath = "resourceRoot";
        string resourcePath = Path.Combine(rootPath, "resource");
        string filePath = Path.Combine(resourcePath, "test.txt");
        Directory.CreateDirectory(rootPath);
        Directory.CreateDirectory(resourcePath);
        string expected = "System Test.";
        using(FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            await stream.WriteAsync(Encoding.UTF8.GetBytes(expected), 0, expected.Length);
        }
        var resourceMemoryService = new ResourceMemoryService(rootPath);

        var memory = await resourceMemoryService.GetResourceAsync("test.txt");
        string actual = Encoding.UTF8.GetString(memory.ToArray());

        Assert.NotNull(memory);
        Assert.Equal(expected, actual);

        Directory.Delete(rootPath, true);
    }
}