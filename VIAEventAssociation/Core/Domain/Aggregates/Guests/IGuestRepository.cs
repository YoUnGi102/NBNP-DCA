using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Repository;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Events;

public interface IGuestRepository : IGenericRepository<Guest>
{
    public Task<Guest> GetByEmailAsync(string email);
}