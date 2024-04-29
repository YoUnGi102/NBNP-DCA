using System;
using System.Collections.Generic;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public partial class Invitation
{
    public string Id { get; set; } = null!;

    public int Status { get; set; }

    public string EventId { get; set; } = null!;

    public string? GuestId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Guest? Guest { get; set; }
}
