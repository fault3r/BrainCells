using System;
using Microsoft.AspNetCore.Http;

namespace BrainCells.Application.Services.TodoRepository.Dto;

public class TodoListDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public IFormFile Picture { get; set; }
}
