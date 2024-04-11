using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RemoveParticipationCommand
{
    public string Email { get; }
    public int EventId { get; }

    public static Result<RemoveParticipationCommand> Create(string email, int eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<RemoveParticipationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        if (eventId <= 0)
            return ResultFailure<RemoveParticipationCommand>.CreateMessageResult(null, ["EventId must be greater than 0."]);

        return ResultSuccess<RemoveParticipationCommand>.CreateSimpleResult(new RemoveParticipationCommand(email, eventId));
    }

    private RemoveParticipationCommand(string email, int eventId)
        => (Email, EventId) = (email, eventId);
}