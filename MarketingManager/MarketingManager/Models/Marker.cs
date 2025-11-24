using System;
using System.Collections.Generic;

namespace MarketingManager.Models;

public partial class Marker
{
    public string UserId { get; set; } = null!;

    public string MarkerId { get; set; } = null!;

    public string? ContactId { get; set; }

    public string? LocationId { get; set; }

    public string? ImageId { get; set; }
}
