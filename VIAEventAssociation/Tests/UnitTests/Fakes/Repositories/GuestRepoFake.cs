using Domain.Aggregates.Guests;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.Events;

namespace UnitTests.Fakes;

public class GuestRepoFake : IGuestRepository
{
    private Guest[] Guests { get; } =
    [
        new Guest(1, "Guest1"),
        new Guest(2, "Guest2"),
        new Guest(3, "Guest3")
    ];

    public async Task<Guest?> GetAsync(int id)
    {
        return await Task.FromResult(Guests.FirstOrDefault(g => g.Id == id));
    }

    public async Task<Guest> SaveAsync(Guest guest)
    {
        return await Task.FromResult(guest);
    }
}