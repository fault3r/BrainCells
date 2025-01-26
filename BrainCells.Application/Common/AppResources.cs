using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Application.Common;

public static class AppResources
{
    public static MemoryStream GetResource(IWebHostEnvironment webHost, string resource)
    {
        string path = string.Empty;
        switch (resource)
        {
            case AppResources.AppLogo:
                path = Path.Combine(webHost.WebRootPath, "resource", AppResources.AppLogo);
                break;
            case AppResources.ProfilePicture:
                path = Path.Combine(webHost.WebRootPath, "resource", AppResources.ProfilePicture);
                break;
        }
        return getResource(path);
    }

    public const string AppLogo = "app-logo.png";
    public const string ProfilePicture = "profile-picture.png";

    private static MemoryStream getResource(string path)
    {
        FileStream file = new FileStream(path, FileMode.Open);
        MemoryStream picture = new();
        file.CopyTo(picture);
        file.Close();
        picture.Close();
        return picture;
    }
}