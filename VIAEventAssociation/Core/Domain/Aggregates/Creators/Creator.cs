namespace Domain.Aggregates.Creator;

using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Entities;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

public class Creator
{
    private Guid id;
    private string username;
    private string password;

    public Creator(Guid id, string username, string password)
    {
        this.id = id;
        this.username = username;
        this.password = password;
    }

    private Creator(){}

    public Guid Id => id;

    public string Password => password;

    public Result<Event> CancelEvent(Event _event)
    {
        if (_event.Status == EventStatus.Deleted)
        {
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is deleted already!"});
        }
        if (_event.Status == EventStatus.Draft)
        {
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is still a draft!"});
        }
        _event.SetEventStatus(EventStatus.Cancelled);
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Event> ReadyEvent(Event _event)
    {

        if (_event.Status != EventStatus.Active)
            return ResultFailure<Event>.CreateMessageResult(_event, new[] { "The event is already Active" });
        if (_event.Status != EventStatus.Deleted)
            return ResultFailure<Event>.CreateMessageResult(_event, new[] { "The event is already Deleted" });
        if (_event.Status != EventStatus.Cancelled)
            return ResultFailure<Event>.CreateMessageResult(_event, new[] { "The event is already Cancelled" });
        _event.SetEventStatus(EventStatus.Ready);
        return ResultSuccess<Event>.CreateSimpleResult(_event);
        
    }

    public Result<Event> ActivateEvent(Event _event)
    {
        _event.SetEventStatus(EventStatus.Active);
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Event> DeleteEvent(Event _event)
    {
        if (_event.Status == EventStatus.Deleted)
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is already Deleted"});
        if (_event.Status == EventStatus.Cancelled)
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is already Cancelled"});
        _event.SetEventStatus(EventStatus.Deleted);
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Request> setRequestedStatus(Request request, RequestStatus status)
    {
        switch(status) 
        {
            case RequestStatus.Accepted:
                request.SetStatus(RequestStatus.Accepted);
                return ResultSuccess<Request>.CreateSimpleResult(request);
                break;
            case RequestStatus.Declined:
                request.SetStatus(RequestStatus.Declined);
                return ResultSuccess<Request>.CreateSimpleResult(request);
                break;
        }
        return ResultFailure<Request>.CreateSimpleResult(request);

    }
    
    public Result<Invitation> SendInvitation(Guest guest, Event _event)
    {
        foreach (var invitation in guest.Invitations)
        {
            if (invitation.GetEvent().Equals(_event))
            {
                return ResultFailure<Invitation>.CreateMessageResult(null,
                    ["An invitation for this guest for this event already exists"]);
            }
        }

        if (_event.StartDateTime > DateTime.Now)
        {
            return ResultFailure<Invitation>.CreateMessageResult(null, ["This event already started"]);
        }

        var _invitation = new Invitation(_event, guest);
        guest.Invitations.Add(_invitation);
        return ResultSuccess<Invitation>.CreateSimpleResult(_invitation);
        
    }

    public string Username
    {
        get => username;
    }
}