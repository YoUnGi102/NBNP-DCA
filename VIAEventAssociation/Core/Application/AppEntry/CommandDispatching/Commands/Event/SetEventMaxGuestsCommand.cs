using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class SetEventMaxGuestsCommand
{
    public Guid Id { get; }
    public int MaxGuests { get; }

    public static Result<SetEventMaxGuestsCommand> Create(Guid id, int maxGuests)
    {
        if (maxGuests < 0 || maxGuests > 1000)
            return ResultFailure<SetEventMaxGuestsCommand>.CreateMessageResult(null, ["Invalid maximum number of guests."]);
        return ResultSuccess<SetEventMaxGuestsCommand>.CreateSimpleResult(new SetEventMaxGuestsCommand(id, maxGuests));
    }

    private SetEventMaxGuestsCommand(Guid id, int maxGuests)
        => (Id, MaxGuests) = (id, maxGuests);
}