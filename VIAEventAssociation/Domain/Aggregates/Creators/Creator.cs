using Domain.Common.Enums;

namespace Domain.Aggregates.Creator;

public class Creator
{
    private int id;
    private string username;
    private string password;

    public void CancelEvent()
    {
    }
    
    public void ReadyEvent(){}
    
    public void ActiveEvent(){}
    
    public void DeleteEvent(){}
    
    public void setRequestedStatus(RequestStatus status){}
}