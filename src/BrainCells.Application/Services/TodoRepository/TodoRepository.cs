using System;
using System.Drawing;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.ResourceMemoryService;
using BrainCells.Application.Services.TodoRepository.Dto;
using BrainCells.Domain.Entities.Todo;

namespace BrainCells.Application.Services.TodoRepository;

public class TodoRepository : ITodoRepository
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IResourceMemoryService _resourceMemoryService;

    public TodoRepository(IDatabaseContext databaseContext,
        IResourceMemoryService resourceMemoryService)
    {
        _databaseContext = databaseContext;
        _resourceMemoryService = resourceMemoryService;
    }

    public async Task<ResultDto> AddListAsync(TodoListDto list)
    {
        try{
            TodoList tList = new TodoList{
                Name = list.Name,
                Description = list.Description,
                Color = list.Color,
                Picture = (await _resourceMemoryService.GetResourceAsync(ResourceMemoryItems.TodoList)).ToArray(),
            };
            _databaseContext.TodoLists.Add(tList);
            await _databaseContext.SaveChangesAsync();
            return new ResultDto{
                Success = true,
                Message = "List add successfully.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "error!",
            };
        }
    }
}
