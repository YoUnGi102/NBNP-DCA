using Domain.Aggregates.Guests;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Common.Entities;

using System.Collections.Generic;
using Domain.Aggregates.Events;
using Domain.Common.Enums;

public class Invitation
{
    public Guid Id { get; private set; }
    public InvitationStatus Status { get; set; }
    public Event Event { get; private set; }
    internal Guest Guest { get; private set; }

    public Invitation(InvitationStatus status, Event @event)
    {
        Status = status;
        Event = @event;
    }

    public Invitation(Event @event, Guest guest)
    {
        Guest = guest;
        Event = @event;
        Status = InvitationStatus.Unanswered;
    }
    
    private Invitation(){}
    
}