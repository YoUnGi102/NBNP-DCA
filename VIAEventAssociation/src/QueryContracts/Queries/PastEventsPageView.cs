using QueryContracts.Contract;

namespace QueryContracts.Queries;

public class PastEventsPageView
{
    public record Query() : IQuery<Answer>;
    public record Answer(PastEvents Guest);
    public record PastEvents(string EventName, string EventDescription, string EventDate, string EventLocation, int ParticipantsCount, string MaxParticipantsCount);
}