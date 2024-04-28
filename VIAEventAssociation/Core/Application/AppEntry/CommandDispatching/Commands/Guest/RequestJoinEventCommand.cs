using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RequestJoinEventCommand
{
    public Guid GuestId { get; }
    public Guid EventId { get; }

    public static Result<RequestJoinEventCommand> Create(string guestId, string eventId)
    {
        if (Guid.TryParse(guestId, out Guid gId) && gId != Guid.Empty)
            return ResultFailure<RequestJoinEventCommand>.CreateMessageResult(null, ["GuestId must be a valid Guid"]);

        if (Guid.TryParse(eventId, out Guid eId) && eId != Guid.Empty)
            return ResultFailure<RequestJoinEventCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

        return ResultSuccess<RequestJoinEventCommand>.CreateSimpleResult(new RequestJoinEventCommand(gId, eId));
    }

    private RequestJoinEventCommand(Guid guestId, Guid eventId)
        => (GuestId, EventId) = (guestId, eventId);
}