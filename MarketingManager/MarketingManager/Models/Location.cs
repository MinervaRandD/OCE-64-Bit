using System;
using System.Collections.Generic;

namespace MarketingManager.Models;

public partial class Location
{
    public string UserId { get; set; } = null!;

    public string LocationId { get; set; } = null!;

    public double Lat { get; set; }

    public double Lng { get; set; }

    public string? LocationName { get; set; }

    public string? Comments { get; set; }
}
