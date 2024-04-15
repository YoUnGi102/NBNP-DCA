using Domain.Common.Repository;

namespace Domain.Aggregates.Creator;

public interface ICreatorRepository : IGenericRepository<Creator>
{
    Task<Creator> GetByUsernameAsync(string username);
}