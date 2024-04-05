using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateEventTitleCommand
{
    public int Id { get; }
    public string Title { get; }

    public static Result<UpdateEventTitleCommand> Create(int id, string title)
    {
        // TODO Add validation
        return ResultSuccess<UpdateEventTitleCommand>.CreateSimpleResult(new UpdateEventTitleCommand(id, title));
    }

    private UpdateEventTitleCommand(int id, string title)
        => (Id, Title) = (id, title);
}