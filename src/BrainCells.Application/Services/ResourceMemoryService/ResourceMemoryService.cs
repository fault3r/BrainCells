using System;

namespace BrainCells.Application.Services.ResourceMemoryService;

public struct ResourceMemoryItems{
    public const string AppLogo = "app-logo.png";
    public const string ProfilePicture = "profile-picture.png";
    public const string OtpTemplate = "otp-tmail.html";
    public const string TodoList = "todo-list.png";
}

public class ResourceMemoryService : IResourceMemoryService
{
    private string _rootPath;

    public ResourceMemoryService(string rootPath)
    {
        _rootPath = Path.Combine(rootPath, "resource");
    }

    public async Task<MemoryStream> GetResourceAsync(string filename)
    {
        string logfile = Path.Combine(_rootPath, filename);
        MemoryStream memory = new();
        using(FileStream file = new FileStream(logfile, FileMode.Open))
        {
            await file.CopyToAsync(memory);
        }
        memory.Close();
        return memory;
    }
}