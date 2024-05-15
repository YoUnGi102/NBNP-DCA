using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventStatusCommand
{
    public Guid Id { get; }
    public EventStatus Status { get; }

    public static Result<SetEventStatusCommand> Create(Guid id, string status)
    {
        if (!Enum.TryParse(status, out EventStatus parsedStatus))
        {
            return ResultFailure<SetEventStatusCommand>.CreateMessageResult(null, ["Incorrect Status type"]);
        }

        return ResultSuccess<SetEventStatusCommand>.CreateSimpleResult(new SetEventStatusCommand(id, parsedStatus));
    }

    private SetEventStatusCommand(Guid id, EventStatus parsedStatus)
        => (Id, Status) = (id, parsedStatus);
}