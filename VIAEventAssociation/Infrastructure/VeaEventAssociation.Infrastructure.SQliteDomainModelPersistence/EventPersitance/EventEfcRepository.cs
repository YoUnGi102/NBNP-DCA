using Domain.Aggregates.Events;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.EventPersitance;

public class EventEfcRepository(DmContext context) : BaseEfcRepository<Domain.Aggregates.Events.Event>(context), IEventRepository
{
    private DmContext context = context;

    public override async Task<Domain.Aggregates.Events.Event> GetAsync(Guid id)
    {
        return await context.Set<Domain.Aggregates.Events.Event>().Include(e => e.Guests).SingleAsync(e => e.Id.Equals(id)) ?? throw new InvalidOperationException();
    }
}