namespace Domain.Aggregates.Events;

public interface IEventRepository
{
    public Task<Event?> GetAsync(int id);

    public Task<Event?> SaveAsync(Event e);
}