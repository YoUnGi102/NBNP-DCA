﻿using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;

namespace UnitTests.Fakes;

public class EventRepoFake : IEventRepository
{

    private Event[] Events { get; } =
    [
        Constants.TEST_EVENT,
        new Event("016f2b81-64bd-489c-9ffa-f98d6ce69c8a", "Title", "Description", DateTime.Now, DateTime.Now.AddHours(2), 30, EventVisibility.Public,
            EventStatus.Active, [], new Location("location", 32))
    ];

    public async Task<Event?> GetAsync(Guid id)
    {
        return await Task.FromResult(Events.FirstOrDefault(e => e.Id.Equals(id)));
    }
    
    public async Task AddAsync(Event e)
    {
        await Task.FromResult(e);
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}