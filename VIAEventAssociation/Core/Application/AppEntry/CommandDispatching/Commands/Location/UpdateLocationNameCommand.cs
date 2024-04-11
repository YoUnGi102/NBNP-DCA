using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateLocationNameCommand
{
    public int Id { get; }
    public string Name { get; }

    public static Result<UpdateLocationNameCommand> Create(int id, string name)
    {
        if (id <= 0 || string.IsNullOrWhiteSpace(name) || name.Length > 30 || name.Length < 3)
            return ResultFailure<UpdateLocationNameCommand>.CreateMessageResult(null, ["Name has to be between 3 and 30 characters long."]);
        return ResultSuccess<UpdateLocationNameCommand>.CreateSimpleResult(new UpdateLocationNameCommand(id, name));
    }

    private UpdateLocationNameCommand(int id, string name)
        => (Id, Name) = (id, name);
}