using QueryContracts.Contract;

namespace QueryContracts.Queries;

public abstract class MyEventsPageView
{
    public record Query() : IQuery<Answer>;
    public record Answer(List<MyEvents> Guest);
    public record MyEvents(string EventName, string EventDescription, 
        string EventDate, string EventLocation, 
        int ParticipantsCount, string MaxParticipantsCount);
}