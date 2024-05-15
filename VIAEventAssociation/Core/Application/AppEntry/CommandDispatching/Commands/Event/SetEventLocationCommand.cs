using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventLocationCommand
{
    public Guid EventId { get; }
    public Guid LocationId { get; }

    public static Result<SetEventLocationCommand> Create(Guid eventId, Guid locationId)
    {
        return ResultSuccess<SetEventLocationCommand>.CreateSimpleResult(new SetEventLocationCommand(eventId, locationId));
    }

    private SetEventLocationCommand(Guid eventId, Guid locationId)
        => (EventId, LocationId) = (eventId, locationId);
}