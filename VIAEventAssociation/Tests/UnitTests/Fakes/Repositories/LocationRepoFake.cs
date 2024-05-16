using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;

namespace UnitTests.Fakes;

public class LocationRepoFake : ILocationRepository
{
    private List<Location> Locations { get; } = [
        new Location(Guid.Empty, "VIA University College", 32),
    ];

    public async Task<Location?> GetAsync(Guid id)
    {
        return await Task.FromResult(Locations.FirstOrDefault(e => e.Id == id));
    }

    public Task AddAsync(Location e)
    {
        Locations.Add(e);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Guid id)
    {
        Locations.Remove(Locations.FirstOrDefault(e => e.Id == id) ?? throw new InvalidOperationException());
        return Task.CompletedTask;
    }
}