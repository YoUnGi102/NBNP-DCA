using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventLocationCommand
{
    public Guid EventId { get; }
    public Guid LocationId { get; }

    public static Result<SetEventLocationCommand> Create(string eventId, string locationId)
    {
        Guid lId, eId;
        if (!Guid.TryParse(eventId, out eId) || !Guid.TryParse(locationId, out lId))
        {
            return ResultFailure<SetEventLocationCommand>.CreateMessageResult(null, ["EventId and LocationId must be valid Guid"]);
        }

        return ResultSuccess<SetEventLocationCommand>.CreateSimpleResult(new SetEventLocationCommand(eId, lId));
    }

    private SetEventLocationCommand(Guid eventId, Guid locationId)
        => (EventId, LocationId) = (eventId, locationId);
}