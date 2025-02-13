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
                Message = "Your message has been sent successfully.",
            };
        }
        catch{
            return new ResultDto{
                Success = false,
                Message = "Failed to send email. That's all we know!",
            };
        }
    }    
}