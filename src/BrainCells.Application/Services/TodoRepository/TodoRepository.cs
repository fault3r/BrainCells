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
    public async Task<IEnumerable<ListDto>?> GetListsAsync(string? id=null)
    {
        try{
            IEnumerable<TodoList> tLists;
            if(id == null)
                tLists = await _databaseContext.TodoLists.ToListAsync();
            else
                tLists = await _databaseContext.TodoLists.Where(p => p.Id.ToString() == id).ToListAsync();
            var lists = tLists.Select(r => new ListDto{
                Id = r.Id.ToString(),
                Name = r.Name,
                Description = r.Description,
                Color = r.Color,
                Picture = Convert.ToBase64String(r.Picture),
            }).ToList();
            return lists;
        }
        catch{
            return null;
        }
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
                Message = "Your list has been created.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "An unexpected error has occurred!",
            };
        }
    }

    public async Task<ResultDto> EditListAsync(EditListDto list)
    {
        try{
            var tList = _databaseContext.TodoLists.Where(p => p.Id.ToString() == list.Id).FirstOrDefault(); 
            tList.Name = list.Name;
            tList.Description = list.Description;
            tList.Color = list.Color;
            if(list.DefaultPicture)
                tList.Picture = (await _resourceMemoryService.GetResourceAsync(ResourceMemoryItems.TodoList)).ToArray();
            else
                if(list.Picture != null)
                    tList.Picture = (await ImageResizer.ResizeAsync(list.Picture, 500, 500)).ToArray();
            _databaseContext.TodoLists.Update(tList);
            await _databaseContext.SaveChangesAsync();
            return new ResultDto{
                Success = true,
                Message = "Your list has been updated.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "An unexpected error has occurred!",
            };
        }
    }
}
