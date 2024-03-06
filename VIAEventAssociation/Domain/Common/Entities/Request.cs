namespace Domain.Common.Entities;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;

public class Request
{
    public RequestStatus status;
    private Guest guest;
    private Event _event;
    
    public Request(RequestStatus status)
    {
        this.status = status;
    }
    public void SetGuest(Guest guest)
    {
        this.guest = guest;
    }
    public Guest GetGuest()
    {
        return this.guest;
    }
    public void SetEvent(Event _event)
    {
        this._event = _event;
    }
    public Event GetEvent()
    {
        return this._event;
    }
}