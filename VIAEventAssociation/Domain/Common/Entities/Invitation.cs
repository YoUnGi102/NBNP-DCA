﻿using Domain.Aggregates.Events;
using Domain.Common.Enums;

namespace Domain.Common.Entities;

public class Invitation
{
    internal InvitationStatus status;
    internal List<Event> events;

    public Invitation(InvitationStatus status, List<Event> events)
    {
        this.status = status;
        this.events = events;
    }
    public InvitationStatus GetStatus()
    {
        return this.status;
    }
    public List<Event> GetEvents()
    {
        return this.events;
    }
}