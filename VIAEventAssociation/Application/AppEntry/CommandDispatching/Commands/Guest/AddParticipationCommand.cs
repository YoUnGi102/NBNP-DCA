using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class AddParticipationCommand
{
    public string Email { get; }
    public int EventId { get; }

    public static Result<AddParticipationCommand> Create(string email, int eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<AddParticipationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        if (eventId <= 0)
            return ResultFailure<AddParticipationCommand>.CreateMessageResult(null, ["EventId must be greater than 0."]);

        return ResultSuccess<AddParticipationCommand>.CreateSimpleResult(new AddParticipationCommand(email, eventId));
    }

    private AddParticipationCommand(string email, int eventId)
        => (Email, EventId) = (email, eventId);
}