using System;
using System.Collections.Generic;

namespace VeaEventAssocition.Infrastructure.EfcQueries;

public partial class Event
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string StartDateTime { get; set; } = null!;

    public string EndDateTime { get; set; } = null!;

    public int MaxGuests { get; set; }

    public int Visibility { get; set; }

    public int Status { get; set; }

    public string LocationId { get; set; } = null!;

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
