using System;
using System.Collections.Generic;

namespace MarketingManager.Models;

public partial class Contact
{
    public string UserId { get; set; } = null!;

    public string ContactId { get; set; } = null!;

    public string? PointOfContact { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? CompanyAddress1 { get; set; }

    public string? CompanyAddress2 { get; set; }

    public string? CompanyCity { get; set; }

    public string? CompanyStateOrRegion { get; set; }

    public string? CompanyCountry { get; set; }

    public string? CompanyPostalCode { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactWebSite { get; set; }

    public string? Comments { get; set; }

    public string? MarkerId { get; set; }

    public string? LocationId { get; set; }

    public string? Status { get; set; }
}
