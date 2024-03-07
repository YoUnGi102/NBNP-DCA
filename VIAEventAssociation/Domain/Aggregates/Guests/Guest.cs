using System;
using System.Collections.Generic;
using Domain.Aggregates.Events;
using Domain.Common.Entities;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;


namespace Domain.Aggregates.Guests;

public class Guest
{
    private string email { get; init; }
    private List<Request> requests { get; init; }
    private List<Invitation> invitations;

    public Guest(string email)
    {
        this.invitations = new List<Invitation>();
        this.email = email;
        this.requests = new List<Request>();
    }
    
    public Guest(string email, List<Request> requests, List<Invitation> invitations)
    {
        this.email = email;
        this.requests = requests;
        this.invitations = invitations;
    }

    public Result<Event> Participate(Event _event)
    {
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Event> RemoveParticipation(Event _event, Guest guest)
    {
        if (!_event.GetGuests().Contains(guest))
        {
            return ResultFailure<Event>.CreateMessageResult(_event, new[] { "The guest isn't assigned to" +
                                                                     " this event!" });
        }
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Event> RequestToJoin(Event _event)
    {
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Invitation> AcceptInvitation(Invitation invitation)
    {
        var _event = invitation.events;
        if (_event.GetEndDateTime() < DateTime.Now)
        {
            invitation.status = InvitationStatus.Declined;
            return ResultFailure<Invitation>.CreateMessageResult(invitation, new []{"The event has already" +
                " finished!"});
        }

        if (_event.GetMaxGuests() == _event.GetGuests().Count)
        {
            invitation.status = InvitationStatus.Declined;
            return ResultFailure<Invitation>.CreateMessageResult(invitation, new []{"The event doesn't have" +
                " any spots left!"});
        }
        _event.AddGuest(this);
        invitation.status = InvitationStatus.Accepted;
        return ResultSuccess<Invitation>.CreateSimpleResult(invitation);
    }

    public Result<Invitation> DeclineInvitation(Invitation invitation)
    {
        invitation.status = InvitationStatus.Declined;
        return ResultSuccess<Invitation>.CreateSimpleResult(invitation);
    }
    
    public List<Request> GetRequests()
    {
        return this.requests;
    }
    public List<Invitation> GetInvitations()
    {
        return this.invitations;
    }
    public void SetInvitations(List<Invitation> invitations)
    {
        this.invitations = invitations;
    }
}