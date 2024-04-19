namespace Domain.Common.Entities;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;

public class Request
{
    public int Id { get; }
    public RequestStatus Status { get; private set; }
    public Guest? Guest { get; private set; }
    public Event? Event { get; private set; }

    public Request(RequestStatus status)
    {
        Status = status;
        Guest = null;
        Event = null;
    }

    public Request(Guest guest, Event _event)
    {
        Guest = guest;
        Event = _event;
        Status = RequestStatus.Unanswered;
    }

    // Needed for EFC
    private Request(){}
    
    public void SetGuest(Guest guest)
    {
        Guest = guest;
    }
    
    public void SetEvent(Event _event)
    {
        Event = _event;
    }

    public void SetStatus(RequestStatus status)
    {
        Status = status;
    }

}