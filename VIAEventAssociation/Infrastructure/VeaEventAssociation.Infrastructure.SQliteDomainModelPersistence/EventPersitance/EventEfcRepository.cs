using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.EventPersitance;

public class EventEfcRepository(DmContext context) : BaseEfcRepository<Event>(context), IEventRepository
{
    private DmContext context = context;

    public override async Task<Event> GetAsync(Guid id)
    {
        return await context.Set<Event>().Include(e => e.Guests).SingleAsync(e => e.Id == id) ?? throw new InvalidOperationException();
    }
}