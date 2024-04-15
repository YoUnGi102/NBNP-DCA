using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.GuestPersistance;

public class GuestEfcRepository(DmContext context) : BaseEfcRepository<Guest>(context), IGuestRepository
{
    private DmContext context = context;

    public async Task<Guest> GetByEmailAsync(string email)
    {
        return await context.Set<Guest>().FirstOrDefaultAsync(g => g.Email == email) ?? throw new InvalidOperationException(); 
    }

}