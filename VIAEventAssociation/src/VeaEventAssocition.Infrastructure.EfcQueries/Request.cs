using System;
using System.Collections.Generic;

namespace VeaEventAssocition.Infrastructure.EfcQueries;

public partial class Request
{
    public int Id { get; set; }

    public int Status { get; set; }

    public string? GuestId { get; set; }

    public string? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Guest? Guest { get; set; }
}
