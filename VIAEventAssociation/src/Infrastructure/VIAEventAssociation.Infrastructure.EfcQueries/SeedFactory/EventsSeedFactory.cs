using Newtonsoft.Json;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactory;

public class EventsSeedFactory
{
    public static List<Event>? CreateEvents()
    {
        string jsonContent = File.ReadAllText("../JSONS/Events.json");
        List<Event>? events = JsonConvert.DeserializeObject<List<Event>>(jsonContent);
        return events;
    }
}