using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventLocationCommand
{
    public int EventId { get; }
    public int LocationId { get; }

    public static Result<SetEventLocationCommand> Create(int eventId, int locationId)
    {
        if (eventId <= 0 || locationId <= 0)
            return ResultFailure<SetEventLocationCommand>.CreateMessageResult(null, ["EventId and LocationId must be greater than 0"]);

        return ResultSuccess<SetEventLocationCommand>.CreateSimpleResult(new SetEventLocationCommand(eventId, locationId));
    }

    private SetEventLocationCommand(int eventId, int locationId)
        => (EventId, LocationId) = (eventId, locationId);
}