using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class SetEventMaxGuestsCommand
{
    public int Id { get; }
    public int MaxGuests { get; }

    public static Result<SetEventMaxGuestsCommand> Create(int id, int maxGuests)
    {
        if (id <= 0 || maxGuests < 0 || maxGuests > 1000)
            return ResultFailure<SetEventMaxGuestsCommand>.CreateMessageResult(null, ["Invalid event id or maximum number of guests."]);
        return ResultSuccess<SetEventMaxGuestsCommand>.CreateSimpleResult(new SetEventMaxGuestsCommand(id, maxGuests));
    }

    private SetEventMaxGuestsCommand(int id, int maxGuests)
        => (Id, MaxGuests) = (id, maxGuests);
}