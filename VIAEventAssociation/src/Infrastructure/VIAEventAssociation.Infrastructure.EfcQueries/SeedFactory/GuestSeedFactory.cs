using Newtonsoft.Json;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactory;

public class GuestSeedFactory
{
    public static List<Guest>? CreateGuests()
    {
        string jsonContent = File.ReadAllText("../JSONS/Guests.json");
        List<Guest>? guests = JsonConvert.DeserializeObject<List<Guest>>(jsonContent);
        return guests;
    }
}