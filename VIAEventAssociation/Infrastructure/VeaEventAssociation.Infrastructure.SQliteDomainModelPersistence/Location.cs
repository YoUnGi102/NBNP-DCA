using System;
using System.Collections.Generic;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public partial class Location
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int MaxCapacity { get; set; }

    public string AvailabilityStart { get; set; } = null!;

    public string? AvailabilityEnd { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
