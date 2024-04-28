using System;
using System.Collections.Generic;
using Domain.Aggregates.Events;
using Domain.Common.Entities;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;


namespace Domain.Aggregates.Guests;

public class Guest
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public List<Request> Requests { get; private set; }
    public List<Invitation> Invitations { get; private set; }
    public string ProfilePicURL { get; private set; }
    public List<Event> Events { get; private set; } = [];
    
    public Guest(string email, string firstName, string lastName, string? profilePicUrl)
    {
        Id = Guid.NewGuid();
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicURL = profilePicUrl ?? "";
        Invitations = [];
        Requests = [];
    }
    
    public Guest(string id, string email, string firstName, string lastName, string? profilePicUrl)
    {
        Id = Guid.TryParse(id, out var guid) ? guid : Guid.NewGuid();
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicURL = profilePicUrl ?? "";
        Invitations = [];
        Requests = [];
    }
    
    public Guest(string email, string profilePicUrl)
    {
        Email = email;
        ProfilePicURL = profilePicUrl;
        Invitations = [];
        Requests = [];
    }
    
    public Guest(string email, List<Request> requests, List<Invitation> invitations)
    {
        Email = email;
        Requests = requests;
        Invitations = invitations;
    }
    
    // Needed for EFC
    private Guest(){}

    public Result<None> Participate(Event _event)
    {
        if (_event.Visibility == EventVisibility.Private)
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
        foreach (var request in Requests)
        {
            if (request.Event.Equals(_event))
            {
                return ResultFailure<Request>.CreateMessageResult(null, ["Request was already created"]);
            }
        }
        
        if (_event.MaxGuests <= _event.Guests.Count)
        {
            return ResultFailure<Request>.CreateMessageResult(null, ["Maximum capacity of guests was reached"]);
        }

        Request _request = new Request(this, _event);
        Requests.Add(_request);
        return ResultSuccess<Request>.CreateSimpleResult(_request);
    }

    public Result<Invitation> AcceptInvitation(Invitation invitation)
    {
        var _event = invitation.Event;
        if (_event.EndDateTime < DateTime.Now)
        {
            invitation.Status = InvitationStatus.Declined;
            return ResultFailure<Invitation>.CreateMessageResult(invitation, new []{"The event has already" +
                " finished!"});
        }

        if (_event.MaxGuests == _event.Guests.Count)
        {
            invitation.Status = InvitationStatus.Declined;
            return ResultFailure<Invitation>.CreateMessageResult(invitation, new []{"The event doesn't have" +
                " any spots left!"});
        }
        _event.AddGuest(this);

        Invitation? find = Invitations.FirstOrDefault(i =>
            i?.Event.Id == invitation?.Event.Id);

        if (find is null)
        {
            return ResultFailure<Invitation>.CreateMessageResult(invitation, ["Invitation not found!"]);
        }

        find.Status = InvitationStatus.Accepted;
        return ResultSuccess<Invitation>.CreateSimpleResult(find);
    }

    public Result<Invitation> DeclineInvitation(Invitation invitation)
    {
        Invitation? find = Invitations.FirstOrDefault(i =>
            i?.Event.Id == invitation?.Event.Id);
        if (find is null)
        {
            return ResultFailure<Invitation>.CreateMessageResult(invitation, ["Invitation not found!"]);
        }
        find.Status = InvitationStatus.Declined;
        return ResultSuccess<Invitation>.CreateSimpleResult(invitation);
    }
    
    public Result<None> SendInvitation(Invitation invitation)
    {
        if (invitation.Event.Guests.Contains(invitation.Guest))
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Guest is already participating!"]);
        }

        Invitation? find = Invitations.FirstOrDefault(i =>
            i?.Event.Id == invitation?.Event.Id);
        if (find is not null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Invitation already sent!"]);
        }
        Invitations.Add(invitation);
        return ResultSuccess<None>.CreateMessageResult(new None(), ["Invitation sent!"]);
    }
}