using System;
using System.Collections.Generic;

namespace VIAEventAssociation.Infrastructure.EfcQueries;

public partial class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string StartDateTime { get; set; } = null!;

    public string EndDateTime { get; set; } = null!;

    public int MaxGuests { get; set; }

    public int Visibility { get; set; }

    public int Status { get; set; }

    public int LocationId { get; set; }

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<Request> RequestEventId1Navigations { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestEvents { get; set; } = new List<Request>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
