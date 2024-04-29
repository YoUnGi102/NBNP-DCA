using System.Text.Json;
using VIAEventAssociation.Infrastructure.EfcQueries.SeedData;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactories;

public class LocationSeedFactory
{
    public static List<Location> CreateLocations()
    {
        List<TmpLocation> tmpLocations = JsonSerializer.Deserialize<List<TmpLocation>>(LocationData.LocationsAsJson) ?? throw new InvalidOperationException();
        var locations = tmpLocations.Select(l => new Location{Id = l.Id, Name = l.Name, MaxCapacity = l.MaxCapacity}).ToList();
        return locations;
    }

    public record TmpLocation(string Id, string Name, int MaxCapacity);
}