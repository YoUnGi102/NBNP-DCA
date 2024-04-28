using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventMaxGuestsCommand
{
    public Guid Id { get; }
    public int MaxGuests { get; }

    public static Result<SetEventMaxGuestsCommand> Create(string id, int maxGuests)
    {
        Guid eId;
        if (!Guid.TryParse(id, out eId))
        {
            return ResultFailure<SetEventMaxGuestsCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);
        }
        return ResultSuccess<SetEventMaxGuestsCommand>.CreateSimpleResult(new SetEventMaxGuestsCommand(eId, maxGuests));
    }

    private SetEventMaxGuestsCommand(Guid id, int maxGuests)
        => (Id, MaxGuests) = (id, maxGuests);
}