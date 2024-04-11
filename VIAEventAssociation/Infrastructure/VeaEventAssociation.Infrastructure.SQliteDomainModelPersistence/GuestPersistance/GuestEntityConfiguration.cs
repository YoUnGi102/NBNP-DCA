using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.InvitationPersistance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.RequestPersistance;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.GuestPersistance;

public class GuestEntityConfiguration
{
    public Guid id { get; init; }
    public string email { get; init; }
    internal List<GuestEntityConfiguration> guests = new();
    internal List<InvitationEntityConfiguration> invitations = new();
    internal List<RequestEntityConfiguration> requests = new();
    
    public void AddGuests(params GuestEntityConfiguration[] guests) => this.guests.AddRange(guests);
    public void AddInvitations(params InvitationEntityConfiguration[] invitations) => this.invitations.AddRange(invitations);
    public void AddRequests(params RequestEntityConfiguration[] requests) => this.requests.AddRange(requests);
}