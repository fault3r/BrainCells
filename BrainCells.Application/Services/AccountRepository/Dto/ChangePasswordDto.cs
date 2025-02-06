using System;

namespace BrainCells.Application.Services.AccountRepository;

public class ChangePasswordDto
{
    public string Id { get; set; }

    public string Mode { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
    
}