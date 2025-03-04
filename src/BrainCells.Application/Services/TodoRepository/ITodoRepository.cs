using System;
using BrainCells.Application.Common;
using BrainCells.Application.Services.TodoRepository;

namespace BrainCells.Application.Services.TodoRepository;

public interface ITodoRepository
{
    Task<IEnumerable<ListDto>?> GetListsAsync(string? id=null);
    
    Task<ResultDto> AddListAsync(AddListDto list);
    Task<ResultDto> EditListAsync(EditListDto list);
    Task<ResultDto> DeleteListAsync(string id);
}
