using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateLocationMaxCapacityCommand
{
    public Guid Id { get; }
    public int MaxCapacity { get; }

    public static Result<UpdateLocationMaxCapacityCommand> Create(Guid id, int maxCapacity)
    {
        if (maxCapacity < 0)
            return ResultFailure<UpdateLocationMaxCapacityCommand>.CreateMessageResult(null, ["Max guests must be a positive number."]);
        return ResultSuccess<UpdateLocationMaxCapacityCommand>.CreateSimpleResult(new UpdateLocationMaxCapacityCommand(id, maxCapacity));
    }

    private UpdateLocationMaxCapacityCommand(Guid id, int maxCapacity)
        => (Id, MaxCapacity) = (id, maxCapacity);
}