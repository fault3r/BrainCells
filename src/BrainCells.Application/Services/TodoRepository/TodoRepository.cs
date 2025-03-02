using System;
using System.Drawing;
using System.Text;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.ResourceMemoryService;
using BrainCells.Application.Services.TodoRepository;
using BrainCells.Domain.Entities.Todo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

    public async Task<ResultDto> AddListAsync(AddListDto list)
    {
        try{
            byte[] listPicture;
            if(list.DefaultPicture || list.Picture == null)
                listPicture = (await _resourceMemoryService.GetResourceAsync(ResourceMemoryItems.TodoList)).ToArray();
            else
                listPicture = (await ImageResizer.ResizeAsync(list.Picture, 500, 500)).ToArray();
            var tList = new TodoList{
                Name = list.Name,
                Description = list.Description,
                Color = list.Color,
                Picture = listPicture,
            };
            _databaseContext.TodoLists.Add(tList);
            await _databaseContext.SaveChangesAsync();
            return new ResultDto{
                Success = true,
                Message = "Your list has been created! You can now add Tasks.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "An unexpected error has occurred. That's all we know!",
            };
        }
    }

    public async Task<IEnumerable<ListDto>?> GetListsAsync()
    {
        try{
            var lists = await _databaseContext.TodoLists.AsQueryable()
                .Select(r => new ListDto{
                    Id = r.Id.ToString(),
                    Name = r.Name,
                    Description = r.Description,
                    Color = r.Color,
                    Picture = Convert.ToBase64String(r.Picture),
                })
                .ToListAsync();
            return lists;
        }
        catch{
            return null;
        }
    }
}
