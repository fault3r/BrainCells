using System;
using Microsoft.AspNetCore.Http;

namespace BrainCells.Application.Services.TodoRepository;

public class AddListDto
{    
    public string Name { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public IFormFile Picture { get; set; }

    public bool DefaultPicture { get; set; }
}
