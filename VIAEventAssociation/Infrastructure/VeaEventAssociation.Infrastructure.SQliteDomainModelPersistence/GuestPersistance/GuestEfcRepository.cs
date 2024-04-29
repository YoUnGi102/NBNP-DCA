using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.GuestPersistance;

public class GuestEfcRepository(DmContext context) : BaseEfcRepository<Domain.Aggregates.Guests.Guest>(context), IGuestRepository
{
    private DmContext context = context;

    public async Task<Domain.Aggregates.Guests.Guest> GetByEmailAsync(string email)
    {
        return await context.Set<Domain.Aggregates.Guests.Guest>().FirstOrDefaultAsync(g => g.Email == email) ?? throw new InvalidOperationException(); 
    }

}