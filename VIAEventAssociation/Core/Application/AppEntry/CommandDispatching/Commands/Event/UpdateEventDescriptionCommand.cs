using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class UpdateEventDescriptionCommand
{
    public Guid Id { get; }
    public string Description { get; }

    public static Result<UpdateEventDescriptionCommand> Create(Guid id, string description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > 700 || description.Length < 11)
            return ResultFailure<UpdateEventDescriptionCommand>.CreateMessageResult(null, ["Description has to be between 11 and 700 characters long."]);
        return ResultSuccess<UpdateEventDescriptionCommand>.CreateSimpleResult(new UpdateEventDescriptionCommand(id, description));
    }

    private UpdateEventDescriptionCommand(Guid id, string description)
        => (Id, Description) = (id, description);
}