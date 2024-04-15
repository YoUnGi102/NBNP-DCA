using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Repository;

namespace Domain.Aggregates.Events;

public interface ILocationRepository : IGenericRepository<Location>
{
}