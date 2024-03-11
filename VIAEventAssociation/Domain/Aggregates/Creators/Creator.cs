using Domain.Aggregates.Events;
using Domain.Common.Entities;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Creator;

public class Creator
{
    private int id;
    private string username;
    private string password;

    public Creator(int id, string username, string password)
    {
        this.id = id;
        this.username = username;
        this.password = password;
    }

    public Result<Event> CancelEvent(Event _event)
    {
        if (_event.status == EventStatus.Deleted)
        {
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is deleted already!"});
        }
        if (_event.status == EventStatus.Draft)
        {
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is still a draft!"});
        }
        _event.SetEventStatus(EventStatus.Cancelled);
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Event> ReadyEvent(Event _event)
    {

        if (_event.status != EventStatus.Active)
            return ResultFailure<Event>.CreateMessageResult(_event, new[] { "The event is already Active" });
        if (_event.status != EventStatus.Deleted)
            return ResultFailure<Event>.CreateMessageResult(_event, new[] { "The event is already Deleted" });
        if (_event.status != EventStatus.Cancelled)
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
        if (_event.status == EventStatus.Deleted)
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is already Deleted"});
        if (_event.status == EventStatus.Cancelled)
            return ResultFailure<Event>.CreateMessageResult(_event, new []{"The event is already Cancelled"});
        _event.SetEventStatus(EventStatus.Deleted);
        return ResultSuccess<Event>.CreateSimpleResult(_event);
    }

    public Result<Request> setRequestedStatus(Request request, RequestStatus status)
    {
        switch(status) 
        {
            case RequestStatus.Accepted:
                request.status = RequestStatus.Accepted;
                return ResultSuccess<Request>.CreateSimpleResult(request);
                break;
            case RequestStatus.Declined:
                request.status = RequestStatus.Declined;
                return ResultSuccess<Request>.CreateSimpleResult(request);
                break;
        }
        return ResultFailure<Request>.CreateSimpleResult(request);

    }
    
}