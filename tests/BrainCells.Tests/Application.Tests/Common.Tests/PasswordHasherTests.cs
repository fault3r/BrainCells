using System;
using BrainCells.Application.Common;
using Xunit;

namespace BrainCells.Tests.Application.Tests.Common.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void ComputeHash_WhenCalledWithString_ReturnsHash()
    {
        string input = "expected";
        string expected = "zqI91Lh+iwDRn7nMqu+T6XNTxzU+IHDzuvBa6zmV3/Q=";
    
        string actual = PasswordHasher.ComputeHash(input);

        Assert.Equal(expected, actual);
    }
}