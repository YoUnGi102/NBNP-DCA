using QueryContracts.Contract;
using VIAEventAssociation.Infrastructure.EfcQueries;

namespace QueryContracts.Queries;

public class PersonalProfilePageQueryHandler(ViaEventAssociationContext context)
    : IQueryHandler<PersonalProfilePageView.Query, PersonalProfilePageView.Answer>
{
    public async Task<PersonalProfilePageView.Answer> HandleAsync(PersonalProfilePageView.Query query)
    {
        PersonalProfilePageView.GuestInfo = await context.Guests.Select(g =>
                new PersonalProfilePageView.GuestInfo( g.Email, g.Email, g.PictureUrl,
                    g.Events.Select(e => new PersonalProfilePageView.UpcomingEvents(e.EventName, e.EventDescription,
                        e.EventDate, e.EventLocation, e.ParticipantsCount, e.MaxParticipantsCount)).ToList()))
            .FirstOrDefaultAsync();
    }
}