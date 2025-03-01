using System;
using BrainCells.Application.Common;
using BrainCells.Application.Services.TodoRepository.Dto;

namespace BrainCells.Application.Services.TodoRepository;

public interface ITodoRepository
{
    Task<ResultDto> AddListAsync(TodoListDto list);
}
