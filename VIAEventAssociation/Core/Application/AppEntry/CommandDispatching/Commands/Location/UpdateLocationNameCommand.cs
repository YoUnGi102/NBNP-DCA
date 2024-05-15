using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateLocationNameCommand
{
    public Guid Id { get; }
    public string Name { get; }

    public static Result<UpdateLocationNameCommand> Create(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 30 || name.Length < 3)
            return ResultFailure<UpdateLocationNameCommand>.CreateMessageResult(null, ["Name has to be between 3 and 30 characters long."]);
        return ResultSuccess<UpdateLocationNameCommand>.CreateSimpleResult(new UpdateLocationNameCommand(id, name));
    }

    private UpdateLocationNameCommand(Guid id, string name)
        => (Id, Name) = (id, name);
}