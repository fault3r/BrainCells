using System;

namespace BrainCells.Application.Services.AccountRepository;

public class SignUpDto
{
    public string Email { get; set; }

    public string Password { get; set; }
    
    public string Name { get; set; }
}
