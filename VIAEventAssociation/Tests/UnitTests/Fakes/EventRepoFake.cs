using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;

namespace UnitTests.Fakes;

public class EventRepoFake : IEventRepository
{
    
    private Event[] Events { get; } = [];

    public async Task<Event?> GetAsync(int id)
    {
        // TODO Remove
        Location location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
        Event _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public,
            EventStatus.Active, [], location);
        return await Task.FromResult(_event);
        //return await Task.FromResult(Events.FirstOrDefault(e => e.GetId() == id));
    }
    
    public async Task<Event> SaveAsync(Event e)
    {
        return await Task.FromResult(e);
    }
}