using Domain.Aggregates.Creator;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.CreatorPersistance;

public class CreatorEfcRepository(DmContext context): BaseEfcRepository<Domain.Aggregates.Creator.Creator>(context), ICreatorRepository
{
    private DbContext context = context;
    
    public async Task<Domain.Aggregates.Creator.Creator> GetByUsernameAsync(string username)
    {
        var creator = await context.Set<Domain.Aggregates.Creator.Creator>().FirstOrDefaultAsync(c => c.Username == username);
        if (creator == null)
            throw new Exception("Creator not found");
        return creator;
    }
}