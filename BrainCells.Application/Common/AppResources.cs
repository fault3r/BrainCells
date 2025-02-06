using System;
using Microsoft.AspNetCore.Hosting;

namespace BrainCells.Application.Common;

public static class AppResources
{
    public const string AppLogo = "app-logo.png";
    public const string ProfilePicture = "profile-picture.png";
    public const string EmailTemplate = "otp-template.html";
 
    public static MemoryStream GetResource(IWebHostEnvironment webHost, string resource)
    {
        string path = Path.Combine(webHost.WebRootPath, "resource", resource);
        FileStream file = new FileStream(path, FileMode.Open);
        MemoryStream memory = new();
        file.CopyTo(memory);
        file.Close();
        memory.Close();
        return memory;
    }
}