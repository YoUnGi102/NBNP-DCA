using System;
using System.Collections.Generic;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public partial class Guest
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string ProfilePicURL { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
