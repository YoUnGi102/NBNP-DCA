using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateLocationMaxCapacityCommand
{
    public int Id { get; }
    public int MaxCapacity { get; }

    public static Result<UpdateLocationMaxCapacityCommand> Create(int id, int maxCapacity)
    {
        if (id <= 0 || maxCapacity < 0)
            return ResultFailure<UpdateLocationMaxCapacityCommand>.CreateMessageResult(null, ["Max guests must be a positive number."]);
        return ResultSuccess<UpdateLocationMaxCapacityCommand>.CreateSimpleResult(new UpdateLocationMaxCapacityCommand(id, maxCapacity));
    }

    private UpdateLocationMaxCapacityCommand(int id, int maxCapacity)
        => (Id, MaxCapacity) = (id, maxCapacity);
}