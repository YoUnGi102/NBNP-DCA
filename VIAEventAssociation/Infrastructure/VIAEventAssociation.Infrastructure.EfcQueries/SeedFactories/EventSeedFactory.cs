using System.Text.Json;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VIAEventAssociation.Infrastructure.EfcQueries.SeedData;

namespace VIAEventAssociation.Infrastructure.EfcQueries.SeedFactories;

public class EventSeedFactory
{
    public static List<Guest> CreateGuests()
    {
        // List<TmpEvent> tmpGuests = JsonSerializer.Deserialize<List<TmpEvent>>(EventData.EventAsJson) ?? throw new InvalidOperationException();
        // var guests = tmpGuests.Select(g => new Event{ = g.Email}).ToList();
        // return guests;
        return [];
    }

    public record TmpEvent(string Email);
}