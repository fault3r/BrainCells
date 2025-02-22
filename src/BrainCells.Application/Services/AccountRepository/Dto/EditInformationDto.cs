using System;
using Microsoft.AspNetCore.Http;

namespace BrainCells.Application.Services.AccountRepository;

public class EditInformationDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public IFormFile Picture { get; set; }
}
