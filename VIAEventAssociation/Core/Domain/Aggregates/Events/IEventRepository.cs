using Domain.Common.Repository;

namespace Domain.Aggregates.Events;

public interface IEventRepository : IGenericRepository<Event>
{
    
}