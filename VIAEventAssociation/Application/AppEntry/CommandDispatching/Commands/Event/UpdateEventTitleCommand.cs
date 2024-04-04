using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateEventTitleCommand
{
    public string Id { get; }
    public string Title { get; }

    public static Result<UpdateEventTitleCommand> Create(string id, string title)
    {
        // TODO Add validation
        return ResultSuccess<UpdateEventTitleCommand>.CreateSimpleResult(new UpdateEventTitleCommand(id, title));
    }

    private UpdateEventTitleCommand(string id, string title)
        => (Id, Title) = (id, title);
}