namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.LocationPersistance;

public class LocationEntityConfiguration
{
    public Guid id { get; init; }
    public string name { get; init; }
    public int maxCapacity { get; init; }
    
    internal List<LocationEntityConfiguration> locations = new();
    
    public void AddLocations(params LocationEntityConfiguration[] locations) => this.locations.AddRange(locations);
    
}