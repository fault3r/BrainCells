using System;

namespace BrainCells.Domain.Entities.Accounts;

public class ForgotPassword
{
    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }

    public string OnetimePassword { get; set; }
}