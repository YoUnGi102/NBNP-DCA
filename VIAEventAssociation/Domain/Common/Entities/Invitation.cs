using Domain.Aggregates.Guests;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Common.Entities;

using System.Collections.Generic;
using Domain.Aggregates.Events;
using Domain.Common.Enums;

public class Invitation
{
    internal InvitationStatus status;
    internal Event events;
    internal Guest guest;

    public Invitation(InvitationStatus status, Event events)
    {
        this.status = status;
        this.events = events;
    }

    public Invitation(Event events, Guest guest)
    {
        this.guest = guest;
        this.events = events;
        this.status = InvitationStatus.Unanswered;
    }
    
    public InvitationStatus GetStatus()
    {
        return this.status;
    }
    public Event GetEvent()
    {
        return this.events;
    }

    public Guest GetGuest()
    {
        return guest;
    }
}