using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;

namespace UnitTests.Fakes;

public class LocationRepoFake : ILocationRepository
{
    private List<Location> Locations { get; } = [
        new Location(1, "VIA University College", 32, []),
    ];

    public async Task<Location?> GetAsync(int id)
    {
        return await Task.FromResult(Locations.FirstOrDefault(e => e.id == id));
    }

    public Task AddAsync(Location e)
    {
        Locations.Add(e);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(int id)
    {
        Locations.Remove(Locations.FirstOrDefault(e => e.id == id) ?? throw new InvalidOperationException());
        return Task.CompletedTask;
    }
}