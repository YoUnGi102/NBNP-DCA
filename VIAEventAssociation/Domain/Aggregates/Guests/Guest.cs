using System;
using System.Collections.Generic;
using Domain.Aggregates.Events;
using Domain.Common.Entities;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;


namespace Domain.Aggregates.Guests;

public class Guest
{
    private int id { get; init; }
    private string email { get; init; }
    private List<Request> requests { get; init; }
    private List<Invitation> invitations;

    
    public Guest(string email)
    {
        this.invitations = new List<Invitation>();
        this.email = email;
        this.requests = new List<Request>();
    }
    
    public Guest(int id, string email)
    {
        this.id = id;
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
    
    public Guest(int id, string email, List<Request> requests, List<Invitation> invitations)
    {
        this.id = id;
        this.email = email;
        this.requests = requests;
        this.invitations = invitations;
    }

    public Result<None> Participate(Event _event)
    {
        if (_event.GetVisibility() == EventVisibility.Private)
        { 
            return ResultFailure<None>.CreateMessageResult(new None(), ["The event is private!"]);
        }
        
        Result<None> result = _event.AddGuest(this);
        if (result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages());
        }
        return ResultSuccess<None>.CreateSimpleResult(new None());
    }

    public Result<None> RemoveParticipation(Event _event)
    {
        return _event.RemoveGuest(this);
    }

    public Result<Request> RequestToJoin(Event _event)
    {
        foreach (var request in requests)
        {
            if (request.GetEvent().Equals(_event))
            {
                return ResultFailure<Request>.CreateMessageResult(null, ["Request was already created"]);
            }
        }
        
        if (_event.GetMaxGuests() <= _event.GetGuests().Count)
        {
            return ResultFailure<Request>.CreateMessageResult(null, ["Maximum capacity of guests was reached"]);
        }

        Request _request = new Request(this, _event);
        requests.Add(_request);
        return ResultSuccess<Request>.CreateSimpleResult(_request);
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
    
    public string Email => email;
    public int Id => id;

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