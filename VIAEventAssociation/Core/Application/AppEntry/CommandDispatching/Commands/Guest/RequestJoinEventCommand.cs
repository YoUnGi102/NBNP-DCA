using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RequestJoinEventCommand
{
    public Guid GuestId { get; }
    public Guid EventId { get; }

    public static Result<RequestJoinEventCommand> Create(Guid guestId, Guid eventId)
    {
        return ResultSuccess<RequestJoinEventCommand>.CreateSimpleResult(new RequestJoinEventCommand(guestId, eventId));
    }

    private RequestJoinEventCommand(Guid guestId, Guid eventId)
        => (GuestId, EventId) = (guestId, eventId);
}