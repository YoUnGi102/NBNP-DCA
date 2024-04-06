using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;

namespace UnitTests.Fakes;

public class EventRepoFake : IEventRepository
{

    private Event[] Events { get; } =
    [
        new Event(1, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(2), 30, EventVisibility.Public,
            EventStatus.Active, [], new Location("location", 32, []))
    ];

    public async Task<Event?> GetAsync(int id)
    {
        return await Task.FromResult(Events.FirstOrDefault(e => e.GetId() == id));
    }
    
    public async Task<Event> SaveAsync(Event e)
    {
        return await Task.FromResult(e);
    }
}