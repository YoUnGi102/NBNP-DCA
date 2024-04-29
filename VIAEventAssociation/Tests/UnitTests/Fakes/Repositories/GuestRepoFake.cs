using Domain.Aggregates.Guests;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.Events;

namespace UnitTests.Fakes;

public class GuestRepoFake : IGuestRepository
{
    List<Guest> Guests = new()
    {
        Constants.TEST_GUEST,
        new Guest("3c0e909b-b0b4-438c-8885-b37545988871", "test1@email.com", "FirstName1", "LastName1", "https://testurl1.com"),
        new Guest("1bfc7546-551f-40c0-bf1a-be31aee3258e", "test2@email.com", "FirstName2", "LastName2", "https://testurl2.com"),
        new Guest("addd30d1-5a88-40f1-9f91-56577c8a1816", "test3@email.com", "FirstName3", "LastName3", "https://testurl3.com"),
    };

    public async Task<Guest> GetAsync(Guid id)
    {
        return await Task.FromResult(Guests.FirstOrDefault(g => g.Id.Equals(id))) ?? throw new InvalidOperationException();
    }

    public async Task AddAsync(Guest e)
    {
        Guests.Add(e);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(Guid id)
    {
        var guest = Guests.First(g => g.Id.Equals(id));
        Guests.Remove(guest);
        await Task.CompletedTask;
    }
    
    public async Task<Guest> GetByEmailAsync(string email)
    {
        return await Task.FromResult(Guests.FirstOrDefault(g => g.Email == email)) ?? throw new InvalidOperationException();
    }

}