using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;

namespace UnitTests.Fakes;

public class LocationRepoFake : ILocationRepository
{
    private List<Location> Locations { get; } = [
        Constants.TEST_LOCATION,
        new Location( "9fb2d5e8-d4df-459b-b92b-f8e1d7e73ff8","VIA University College", 32),
    ];

    public async Task<Location?> GetAsync(Guid id)
    {
        return await Task.FromResult(Locations.FirstOrDefault(e => e.Id.Equals(id)));
    }

    public async Task AddAsync(Location e)
    {
        Locations.Add(e);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(Guid id)
    {
        Locations.Remove(Locations.FirstOrDefault(e => e.Id.Equals(id)) ?? throw new InvalidOperationException());
        await Task.CompletedTask;
    }
}