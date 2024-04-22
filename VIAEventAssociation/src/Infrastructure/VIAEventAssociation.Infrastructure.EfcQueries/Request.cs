using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class Request
{
    public int Id { get; set; }

    public int Status { get; set; }

    public int? GuestId { get; set; }

    public int? EventId1 { get; set; }

    public int? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Event? EventId1Navigation { get; set; }

    public virtual Guest? Guest { get; set; }
}
