using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateLocationMaxCapacityCommand
{
    public Guid Id { get; }
    public int MaxCapacity { get; }

    public static Result<UpdateLocationMaxCapacityCommand> Create(string id, int maxCapacity)
    {
        if (Guid.TryParse(id, out Guid lId) && lId != Guid.Empty)
            return ResultFailure<UpdateLocationMaxCapacityCommand>.CreateMessageResult(null, ["LocationId must be a valid Guid"]);

        
        if (maxCapacity < 0)
            return ResultFailure<UpdateLocationMaxCapacityCommand>.CreateMessageResult(null, ["Max guests must be a positive number."]);
        return ResultSuccess<UpdateLocationMaxCapacityCommand>.CreateSimpleResult(new UpdateLocationMaxCapacityCommand(lId, maxCapacity));
    }

    private UpdateLocationMaxCapacityCommand(Guid id, int maxCapacity)
        => (Id, MaxCapacity) = (id, maxCapacity);
}