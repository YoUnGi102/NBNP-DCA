using Domain.Aggregates.Guests;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.Events;

namespace UnitTests.Fakes;

public class GuestRepoFake : IGuestRepository
{
    private List<Guest> Guests { get; } =
    [
        new Guest(1, "guest1@gmail.com"),
        new Guest(2, "guest2@gmail.com"),
        new Guest(3, "guest3@gmail.com")
    ];

    public async Task<Guest> GetAsync(int id)
    {
        return await Task.FromResult(Guests.FirstOrDefault(g => g.Id == id)) ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Guest e)
    {
        Guests.Append(e);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(int id)
    {
        var guest = Guests.First(g => g.Id == id);
        Guests.Remove(guest);
        await Task.CompletedTask;
    }

    public async Task<Guest?> GetAsync(string email)
    {
        return await Task.FromResult(Guests.FirstOrDefault(g => g.Email == email));
    }
    
    public async Task<Guest> GetByEmailAsync(string email)
    {
        return await Task.FromResult(Guests.FirstOrDefault(g => g.Email == email)) ?? throw new InvalidOperationException();
    }

}