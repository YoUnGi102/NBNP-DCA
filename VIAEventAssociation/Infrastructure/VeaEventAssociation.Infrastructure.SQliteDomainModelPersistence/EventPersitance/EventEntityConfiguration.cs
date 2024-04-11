using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.GuestPersistance;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.EventPersitance;

public class EventEntityConfiguration
{
    public Guid id { get; init; }
    public string title { get; init; }
    public string description { get; init; }
    public DateTime start_date_time { get; init; }
    public DateTime end_date_time { get; init; }
    public int max_guests { get; init; }
    internal EventVisibility visibility = EventVisibility.Private;
    internal EventStatus status = EventStatus.Draft;
    internal List<GuestEntityConfiguration> guests = new();
    internal Guid locationId;
    public void SetLocationId(Guid locationId) => this.locationId = locationId;
    public void AddGuests(params GuestEntityConfiguration[] guests) => this.guests.AddRange(guests);
    public void SetVisibility(EventVisibility visibility) => this.visibility = visibility;
    public void SetStatus(EventStatus status) => this.status = status;

}