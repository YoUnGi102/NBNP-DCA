using QueryContracts.Contract;

namespace QueryContracts.Queries;

public class UpcomingEventsPageView
{
    public record Query() : IQuery<Answer>;

    public record Answer(UpcomingEvents Guest);
    
    public record UpcomingEvents(string EventName, string EventDescription, string EventDate, string EventLocation, int ParticipantsCount, string MaxParticipantsCount);
}