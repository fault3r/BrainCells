using System;
using BrainCells.Application.Common;
using BrainCells.Application.Interfaces;
using BrainCells.Domain.Entities;

namespace BrainCells.Application.Services.ContactService;

public class ContactService : IContactService
{
    private readonly IDatabaseContext _databaseContext;

    public ContactService(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<ResultDto> SaveMessageAsync(string fullname, string email, string message)
    {
        try{                
            var contact = new Contact{
                FullName =  fullname.Trim(),
                Email = email.ToLower(),
                Message = message,
            };
            _databaseContext.Contacts.Add(contact);
            await _databaseContext.SaveChangesAsync();
            return new ResultDto{
                Success = true,
                Message = "Your message has been successfully sent. We appreciate you contacting us and will respond as soon as possible.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "Unable to connect to the database. That's all we know!",
            };
        }
    }    
}