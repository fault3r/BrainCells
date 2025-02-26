using System;

namespace BrainCells.Application.Services.ResourceMemoryService;

public interface IResourceMemoryService
{
    Task<MemoryStream> GetResourceAsync(string filename);   
}