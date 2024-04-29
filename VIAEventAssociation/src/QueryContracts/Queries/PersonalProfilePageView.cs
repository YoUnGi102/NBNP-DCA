using QueryContracts.Contract;

namespace QueryContracts.Queries;

public abstract class PersonalProfilePageView
{
    public record Query() : IQuery<Answer>;

    public record Answer(GuestInfo Guest);

    public record GuestInfo(string Name, string Email, string PictureUrl, List<UpcomingEvents> Events);
    public record UpcomingEvents(string EventName, string EventDescription, string EventDate, string EventLocation, int ParticipantsCount, string MaxParticipantsCount);
}