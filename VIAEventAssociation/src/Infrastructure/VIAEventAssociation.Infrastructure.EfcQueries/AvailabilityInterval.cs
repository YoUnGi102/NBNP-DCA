using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class AvailabilityInterval
{
    public int Id { get; set; }

    public string StartDateTime { get; set; } = null!;

    public string EndDateTime { get; set; } = null!;

    public int? LocationId { get; set; }

    public virtual Location? Location { get; set; }
}
