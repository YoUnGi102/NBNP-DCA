using Domain.Aggregates.Guests;

namespace Domain.Aggregates.Events;

public interface IGuestRepository
{
    public Task<Guest> GetAsync(int id);

    public Task<Guest> SaveAsync(Event e);
}