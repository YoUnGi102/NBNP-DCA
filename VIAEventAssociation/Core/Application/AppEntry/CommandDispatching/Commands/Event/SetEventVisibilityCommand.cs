using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventVisibilityCommand
{
    public int Id { get; }
    public EventVisibility Visibility { get; }

    public static Result<SetEventVisibilityCommand> Create(int id, string visibility)
    {
        if (id <= 0)
            return ResultFailure<SetEventVisibilityCommand>.CreateMessageResult(null, ["Id must be greater than 0."]);
        if (!Enum.TryParse(visibility, out EventVisibility parsedVisibility))
        {
            return ResultFailure<SetEventVisibilityCommand>.CreateMessageResult(null, ["Incorrect Visibility type"]);
        }
        
        return ResultSuccess<SetEventVisibilityCommand>.CreateSimpleResult(new SetEventVisibilityCommand(id, parsedVisibility));
    }

    private SetEventVisibilityCommand(int id, EventVisibility parsedVisibility)
        => (Id, Visibility) = (id, parsedVisibility);
}