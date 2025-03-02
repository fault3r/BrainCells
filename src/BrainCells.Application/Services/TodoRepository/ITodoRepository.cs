using System;
using BrainCells.Application.Common;
using BrainCells.Application.Services.TodoRepository;

namespace BrainCells.Application.Services.TodoRepository;

public interface ITodoRepository
{
    Task<ResultDto> AddListAsync(AddListDto list);

    Task<IEnumerable<ListDto>?> GetListsAsync();
}
