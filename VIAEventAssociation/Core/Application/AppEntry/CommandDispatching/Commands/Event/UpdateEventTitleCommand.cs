using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class UpdateEventTitleCommand
{
    public int Id { get; }
    public string Title { get; }

    public static Result<UpdateEventTitleCommand> Create(int id, string title)
    {
        if (id <= 0 || string.IsNullOrWhiteSpace(title) || title.Length > 100 || title.Length < 3)
            return ResultFailure<UpdateEventTitleCommand>.CreateMessageResult(null, ["Title has to be between 3 and 100 characters long."]);
        return ResultSuccess<UpdateEventTitleCommand>.CreateSimpleResult(new UpdateEventTitleCommand(id, title));
    }

    private UpdateEventTitleCommand(int id, string title)
        => (Id, Title) = (id, title);
}