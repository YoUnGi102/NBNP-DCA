using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class SetEventStatusCommand
{
    public int Id { get; }
    public EventStatus Status { get; }

    public static Result<SetEventStatusCommand> Create(int id, string status)
    {
        if (id <= 0)
            return ResultFailure<SetEventStatusCommand>.CreateMessageResult(null, ["Id must be greater than 0."]);
        if (!Enum.TryParse(status, out EventStatus parsedStatus))
        {
            return ResultFailure<SetEventStatusCommand>.CreateMessageResult(null, ["Incorrect Status type"]);
        }
        
        return ResultSuccess<SetEventStatusCommand>.CreateSimpleResult(new SetEventStatusCommand(id, parsedStatus));
    }

    private SetEventStatusCommand(int id, EventStatus parsedStatus)
        => (Id, Status) = (id, parsedStatus);
}