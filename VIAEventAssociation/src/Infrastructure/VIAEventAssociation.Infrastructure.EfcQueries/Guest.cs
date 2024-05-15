using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class Guest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
