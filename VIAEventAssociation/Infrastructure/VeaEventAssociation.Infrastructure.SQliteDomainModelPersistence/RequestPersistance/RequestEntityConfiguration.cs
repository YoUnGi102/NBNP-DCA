using Domain.Common.Enums;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.RequestPersistance;

public class RequestEntityConfiguration
{
    public Guid id;
    internal RequestStatus requestStatus = RequestStatus.Unanswered;
    internal Guid guestId;
    internal Guid eventId;
}