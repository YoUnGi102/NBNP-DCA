using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;

namespace Domain.Aggregates.Events;

public interface IGuestRepository
{
    public Task<Guest> GetAsync(int id);

    public Task<Guest> SaveAsync(Location e);
}