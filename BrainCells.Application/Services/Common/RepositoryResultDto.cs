using System;
using BrainCells.Domain.Entities.Accounts;

namespace BrainCells.Application.Services.Common;

public class RepositoryResultDto
{
    public bool Success { get; set; }

    public string? Message { get; set; }
}