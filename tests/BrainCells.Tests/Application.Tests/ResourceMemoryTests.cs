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
        string root = "resourceRoot";
        string resource = Path.Combine(root, "resource");
        string file = Path.Combine(resource, "test.txt");
        Directory.CreateDirectory(root);
        Directory.CreateDirectory(resource);
        string expected = "System Test.";
        using(FileStream xfile = new FileStream(file, FileMode.Create, FileAccess.Write))
        {
            await xfile.WriteAsync(Encoding.UTF8.GetBytes(expected), 0, expected.Length);
        }
        var resourceMemoryService = new ResourceMemoryService(root);

        var memory = await resourceMemoryService.GetResourceAsync("test.txt");
        string actual = Encoding.UTF8.GetString(memory.ToArray());

        Assert.NotNull(memory);
        Assert.Equal(expected, actual);

        Directory.Delete(root, true);
    }
}