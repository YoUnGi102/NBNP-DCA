using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class UpdateEventTitleCommand
{
    public Guid Id { get; }
    public string Title { get; }

    public static Result<UpdateEventTitleCommand> Create(Guid id, string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > 100 || title.Length < 3)
            return ResultFailure<UpdateEventTitleCommand>.CreateMessageResult(null, ["Title has to be between 3 and 100 characters long."]);
        return ResultSuccess<UpdateEventTitleCommand>.CreateSimpleResult(new UpdateEventTitleCommand(id, title));
    }

    private UpdateEventTitleCommand(Guid id, string title)
        => (Id, Title) = (id, title);
}