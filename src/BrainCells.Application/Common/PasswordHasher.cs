using System;
using System.Security.Cryptography;
using System.Text;

namespace BrainCells.Application.Common;

public static class PasswordHasher
{
    public static string ComputeHash(string text)
    {
        var hasher = SHA256.Create();
        var hash  = hasher.ComputeHash(Encoding.UTF8.GetBytes(text));
        return Convert.ToBase64String(hash);
    }
}