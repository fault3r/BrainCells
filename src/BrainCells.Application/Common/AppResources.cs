using System;
using Microsoft.AspNetCore.Hosting;

namespace BrainCells.Application.Common;

public static class AppResources
{
    public const string AppLogo = "app-logo.png";
    public const string ProfilePicture = "profile-picture.png";

    public const string OTPTemplate = "otp-tmail.html";
 
    public async static Task<MemoryStream> GetResourceAsync(IWebHostEnvironment webHost, string resource)
    {
        string path = Path.Combine(webHost.WebRootPath, "resource", resource);
        MemoryStream memory = new();
        using(FileStream file = new FileStream(path, FileMode.Open))
        {
            await file.CopyToAsync(memory);
        }
        memory.Close();
        return memory;
    }
}