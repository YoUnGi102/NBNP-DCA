using Microsoft.EntityFrameworkCore;
using QueryContracts.Contract;
using VIAEventAssociation.Infrastructure.EfcQueries;

namespace QueryContracts.Queries;

public class MyEventsPageQueryHandler : IQueryHandler<MyEventsPageView.Query, MyEventsPageView.Answer>
{
    private readonly ViaEventAssociationContext context;
    
    public MyEventsPageQueryHandler(ViaEventAssociationContext context)
     => this.context = context;


    public async Task<MyEventsPageView.Answer> HandleAsync(MyEventsPageView.Query query)
    {
        var events =  context.Events
            .Select(e => new MyEventsPageView.MyEvents(
                e.Title, e.Description, e.StartDateTime,
                e.Location.ToString(), e.Guests.Count,
                e.MaxGuests.ToString()));
        return new MyEventsPageView.Answer(events.ToList());
    }
}