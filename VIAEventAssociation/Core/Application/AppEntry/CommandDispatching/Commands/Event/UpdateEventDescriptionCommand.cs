using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class UpdateEventDescriptionCommand
{
    public Guid Id { get; }
    public string Description { get; }

    public static Result<UpdateEventDescriptionCommand> Create(string id, string description)
    {
        if (Guid.TryParse(id, out Guid eId) && eId != Guid.Empty)
            return ResultFailure<UpdateEventDescriptionCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

        return ResultSuccess<UpdateEventDescriptionCommand>.CreateSimpleResult(new UpdateEventDescriptionCommand(eId, description));
    }

    private UpdateEventDescriptionCommand(Guid id, string description)
        => (Id, Description) = (id, description);
}