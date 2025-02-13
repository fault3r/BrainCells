using BrainCells.Application.Common;
using Xunit;

namespace BrainCells.Tests.Application.Common;

public class OnetimePasswordTests
{
    [Fact]
    public void OneTimePassword_Return8DigitPassword()
    {
        string password = OnetimePassword.Create();
        int expected = password.Length;
        Assert.Equal(8, expected);
    }
}