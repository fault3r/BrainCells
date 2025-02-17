using System;

namespace BrainCells.Application.Common;

public static class OnetimePassword
{
    public static string Create()
    {
        Random rnd = new();
        string password = string.Empty;
        for(int i=0; i<8; i++)
        {
            int x, n = rnd.Next(3);
            if(n==0)
                x=rnd.Next(48,58);
            else if(n==1)
                x=rnd.Next(65,91);
            else
                x=rnd.Next(97,123);
            password+=(char)x;
        }
        return password;
    }
}