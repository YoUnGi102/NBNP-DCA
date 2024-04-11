using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;

namespace Domain.Aggregates.Events;

public interface ILocationRepository
{
    public Task<Location> GetAsync(int id);

    public Task<Location> SaveAsync(Location e);
}