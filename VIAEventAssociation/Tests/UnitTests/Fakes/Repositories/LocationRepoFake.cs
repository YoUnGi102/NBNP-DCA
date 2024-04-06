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
    
    public async Task<Location> SaveAsync(Location e)
    {
        Locations.Add(e);
        return e;
    }
}