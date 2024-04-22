using System.Text.Json;
using VIAEventAssociation.Infrastructure.EfcQueries.SeedData;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactories;

public class GuestSeedFactory
{
    public static List<Guest> CreateGuests()
    {
        // List<TmpGuest> tmpGuests = JsonSerializer.Deserialize<List<TmpGuest>>(GuestData.GuestsAsJson) ?? throw new InvalidOperationException();
        // var guests = tmpGuests.Select(g => new Guest{First Email = g.Email}).ToList();
        // return guests;
        return null;
    }

    public record TmpGuest(string FirstName, string LastName, string Email);
}