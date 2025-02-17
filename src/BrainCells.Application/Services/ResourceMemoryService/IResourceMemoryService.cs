using System;

namespace BrainCells.Application.Services.ResourceMemoryService;

public interface IResourceMemoryService
{
    const string AppLogo = "app-logo.png";
    const string ProfilePicture = "profile-picture.png";
    const string OTPTemplate = "otp-tmail.html";

    Task<MemoryStream> GetResourceAsync(string filename);   
}