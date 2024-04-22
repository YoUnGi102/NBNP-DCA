using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class Invitation
{
    public int Id { get; set; }

    public int? Guestid { get; set; }

    public int? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Guest? Guest { get; set; }
}
