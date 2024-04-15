using Domain.Aggregates.Creator;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.CreatorPersistance;

public class CreatorEfcRepository(DmContext context): BaseEfcRepository<Creator>(context), ICreatorRepository
{
    private DbContext context = context;

    public async Task<Creator> GetByUsernameAsync(string username)
    {
        return await context.Set<Creator>().FirstOrDefaultAsync(c => c.Username == username) ?? throw new InvalidOperationException();
    }
}