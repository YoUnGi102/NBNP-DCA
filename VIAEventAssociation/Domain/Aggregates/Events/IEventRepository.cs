namespace Domain.Aggregates.Events;

public interface IEventRepository
{
    public Task<Event> GetAsync(string id);
}