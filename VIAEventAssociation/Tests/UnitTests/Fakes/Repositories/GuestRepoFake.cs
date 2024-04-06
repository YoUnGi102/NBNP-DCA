using Domain.Aggregates.Guests;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.Events;

namespace UnitTests.Fakes;

public class GuestRepoFake : IGuestRepository
{
    private Guest[] Guests { get; } =
    [
        new Guest(1, "guest1@gmail.com"),
        new Guest(2, "guest2@gmail.com"),
        new Guest(3, "guest3@gmail.com")
    ];

    public async Task<Guest?> GetAsync(int id)
    {
        return await Task.FromResult(Guests.FirstOrDefault(g => g.Id == id));
    }

    public Task<Guest?> GetAsync(string email)
    {
        return Task.FromResult(Guests.FirstOrDefault(g => g.Email == email));
    }

    public async Task<Guest> SaveAsync(Guest guest)
    {
        return await Task.FromResult(guest);
    }
}