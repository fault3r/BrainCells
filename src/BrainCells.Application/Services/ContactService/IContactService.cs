using System;
using BrainCells.Application.Common;

namespace BrainCells.Application.Services.ContactService;

public interface IContactService
{
    Task<ResultDto> SaveMessageAsync(string fullname, string email, string message);
    
}