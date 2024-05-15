using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventVisibilityCommand
{
    public Guid Id { get; }
    public EventVisibility Visibility { get; }

    public static Result<SetEventVisibilityCommand> Create(Guid id, string visibility)
    {
        if (!Enum.TryParse(visibility, out EventVisibility parsedVisibility))
        {
            return ResultFailure<SetEventVisibilityCommand>.CreateMessageResult(null, ["Incorrect Visibility type"]);
        }

        return ResultSuccess<SetEventVisibilityCommand>.CreateSimpleResult(
            new SetEventVisibilityCommand(id, parsedVisibility));
    }

    private SetEventVisibilityCommand(Guid id, EventVisibility parsedVisibility)
        => (Id, Visibility) = (id, parsedVisibility);
}