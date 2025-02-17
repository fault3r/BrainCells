using System;
using System.Security.Cryptography;
using System.Text;

namespace BrainCells.Application.Common;

public static class PasswordHasher
{
    public static string ComputeHash(string input)
    {
        var hasher = SHA256.Create();
        var hash  = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hash);
    }
}