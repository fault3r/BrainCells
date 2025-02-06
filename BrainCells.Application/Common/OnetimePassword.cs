using System;

namespace BrainCells.Application.Common;

public static class OnetimePassword
{
    public static string Create()
    {
        Random rnd = new Random();
        string password = string.Empty;
        for(int i=0; i<10; i++)
            password += (char)rnd.Next(33,126);
        return password;
    }
}