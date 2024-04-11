using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RequestJoinEventCommand
{
    public int GuestId { get; }
    public int EventId { get; }

    public static Result<RequestJoinEventCommand> Create(int guestId, int eventId)
    {
        if (guestId <= 0 || eventId <= 0)
            return ResultFailure<RequestJoinEventCommand>.CreateMessageResult(null, ["GuestId and EventId must be greater than 0"]);

        return ResultSuccess<RequestJoinEventCommand>.CreateSimpleResult(new RequestJoinEventCommand(guestId, eventId));
    }

    private RequestJoinEventCommand(int guestId, int eventId)
        => (GuestId, EventId) = (guestId, eventId);
}