using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class CreateLocationCommand
{
    public string Name { get; }
    public int MaxCapacity { get; }

    public static Result<CreateLocationCommand> Create(string name, int maxCapacity)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 30 || name.Length < 3)
            return ResultFailure<CreateLocationCommand>.CreateMessageResult(null, ["Name length is incorrect"]);

        if (maxCapacity <= 0)
            return ResultFailure<CreateLocationCommand>.CreateMessageResult(null, ["Max capacity must be greater than 0"]);

        return ResultSuccess<CreateLocationCommand>.CreateSimpleResult(new CreateLocationCommand(name, maxCapacity));
    }

    private CreateLocationCommand(string name, int maxCapacity)
        => (Name, MaxCapacity) = (name, maxCapacity);
}