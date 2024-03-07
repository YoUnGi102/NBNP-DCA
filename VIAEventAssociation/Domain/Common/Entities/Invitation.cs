using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Common.Entities;

using System.Collections.Generic;
using Domain.Aggregates.Events;
using Domain.Common.Enums;

public class Invitation
{
    internal InvitationStatus status;
    internal Event events;

    public Invitation(InvitationStatus status, Event events)
    {
        this.status = status;
        this.events = events;
    }
    public InvitationStatus GetStatus()
    {
        return this.status;
    }
    public Event GetEvent()
    {
        return this.events;
    }
}