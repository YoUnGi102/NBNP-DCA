using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class UpdateEventTitleCommand
{
    public Guid Id { get; }
    public string Title { get; }

    public static Result<UpdateEventTitleCommand> Create(string id, string title)
    {
        if (Guid.TryParse(id, out Guid eId) && eId != Guid.Empty)
            return ResultFailure<UpdateEventTitleCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

        
        if (string.IsNullOrWhiteSpace(title) || title.Length > 100 || title.Length < 3)
            return ResultFailure<UpdateEventTitleCommand>.CreateMessageResult(null, ["Title has to be between 3 and 100 characters long."]);
        return ResultSuccess<UpdateEventTitleCommand>.CreateSimpleResult(new UpdateEventTitleCommand(eId, title));
    }

    private UpdateEventTitleCommand(Guid id, string title)
        => (Id, Title) = (id, title);
}