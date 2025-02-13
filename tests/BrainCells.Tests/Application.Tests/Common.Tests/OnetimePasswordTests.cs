using Xunit;
using BrainCells.Application.Common;

namespace BrainCells.Tests.Application.Tests.Common.Tests;

public class OnetimePasswordTests
{
    [Fact]
    public void Create_WhenCalled_ReturnsEightDigitPassword()
    {
        string password;
        int expected = 8;

        password = OnetimePassword.Create();
        int actual = password.Length;

        Assert.Equal(expected, actual);
    }
}