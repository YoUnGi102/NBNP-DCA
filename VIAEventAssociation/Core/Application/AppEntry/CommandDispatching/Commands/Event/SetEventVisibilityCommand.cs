using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventVisibilityCommand
{
    public Guid Id { get; }
    public EventVisibility Visibility { get; }

    public static Result<SetEventVisibilityCommand> Create(string id, string visibility)
    {
        if (Guid.TryParse(id, out Guid eId) && eId != Guid.Empty)
            return ResultFailure<SetEventVisibilityCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

        if (!Enum.TryParse(visibility, out EventVisibility parsedVisibility))
        {
            return ResultFailure<SetEventVisibilityCommand>.CreateMessageResult(null, ["Incorrect Visibility type"]);
        }
        
        return ResultSuccess<SetEventVisibilityCommand>.CreateSimpleResult(new SetEventVisibilityCommand(eId, parsedVisibility));
    }

    private SetEventVisibilityCommand(Guid id, EventVisibility parsedVisibility)
        => (Id, Visibility) = (id, parsedVisibility);
}