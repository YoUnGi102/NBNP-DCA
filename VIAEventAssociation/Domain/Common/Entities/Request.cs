using Domain.Common.Enums;

namespace Domain.Common.Entities;

public class Request
{
    public RequestStatus status;
    
    public Request(RequestStatus status)
    {
        this.status = status;
    }
}