using Newtonsoft.Json;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactory;

public class LocationsSeedFactory
{  
    public static List<Location>? CreateLocations()
    {
        string jsonContent = File.ReadAllText("../JSONS/Locations.json");
        List<Location>? locations = JsonConvert.DeserializeObject<List<Location>>(jsonContent);
        return locations;
    }
}