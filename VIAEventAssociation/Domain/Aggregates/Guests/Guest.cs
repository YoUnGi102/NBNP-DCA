using System.Collections.Generic;
using Domain.Aggregates.Events;
using Domain.Common.Entities;
using VIAEventAssociation.Core.Tools.OperationResult.Result;


namespace Domain.Aggregates.Guests;

public class Guest
{
    private string email;
    private List<Request> requests;
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

    public Result<Event> RemoveParticipation(Event _event)
    {
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Event> RequestToJoin(Event _event)
    {
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Invitation> AcceptInvitation(Invitation invitation)
    {
        return ResultSuccess<Invitation>.CreateSimpleResult(invitation);
    }

    public Result<Invitation> DeclineInvitation(Invitation invitation)
    {
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