using Domain.Aggregates.Events;
using Domain.Common.Entities;
using Domain.Common.Enums;

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

    public void CancelEvent(Event _event)
    {
        _event.SetEventStatus(EventStatus.Cancelled);
    }

    public void ReadyEvent(Event _event)
    {
        if (_event.status != EventStatus.Active && _event.status != EventStatus.Deleted && _event.status != 
            EventStatus.Cancelled)
        {
            _event.SetEventStatus(EventStatus.Ready);
        }
    }

    public void ActivateEvent(Event _event)
    {
        _event.SetEventStatus(EventStatus.Active);
    }

    public void DeleteEvent(Event _event)
    {
        if (_event.status != EventStatus.Deleted && _event.status != EventStatus.Cancelled)
        {
            _event.SetEventStatus(EventStatus.Deleted);   
        }
    }

    public void setRequestedStatus(Request request, RequestStatus status)
    {
        switch(status) 
        {
            case RequestStatus.Accepted:
                request.status = RequestStatus.Accepted;
                break;
            case RequestStatus.Declined:
                request.status = RequestStatus.Declined;
                break;
        }
    }
}