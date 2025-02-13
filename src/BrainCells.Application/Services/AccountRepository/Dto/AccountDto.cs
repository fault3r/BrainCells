using System;

namespace BrainCells.Application.Services.AccountRepository;

public class AccountDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string Name { get; set; }

    public string Picture { get; set; }   
}