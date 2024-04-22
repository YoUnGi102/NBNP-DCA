using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MaxCapacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
