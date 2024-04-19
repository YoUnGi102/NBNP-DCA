using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;

namespace UnitTests.Fakes;

public class EventRepoFake : IEventRepository
{

    private Event[] Events { get; } =
    [
        new Event(1, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(2), 30, EventVisibility.Public,
            EventStatus.Active, [], new Location("location", 32))
    ];

    public async Task<Event?> GetAsync(int id)
    {
        return await Task.FromResult(Events.FirstOrDefault(e => e.Id == id));
    }
    
    public async Task AddAsync(Event e)
    {
        await Task.FromResult(e);
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }
}