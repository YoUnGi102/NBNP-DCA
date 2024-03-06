using Domain.Aggregates.Events;
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
    public void CancelEvent(Event _event){}
    
    public void ReadyEvent(Event _event){}
    
    public void ActiveEvent(Event _event){}
    
    public void DeleteEvent(Event _event){}
    
    public void setRequestedStatus(RequestStatus status){}
}