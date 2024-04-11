using Domain.Common.Enums;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.InvitationPersistance;

public class InvitationEntityConfiguration
{
    public Guid id;
    internal InvitationStatus status = InvitationStatus.Unanswered;
    internal Guid eventId;
    internal Guid guestId;
}