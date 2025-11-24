using System;
using System.Collections.Generic;

namespace MarketingManager.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? FirstName { get; set; }
}
