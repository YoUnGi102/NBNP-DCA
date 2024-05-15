using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RemoveParticipationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<RemoveParticipationCommand> Create(string email, Guid eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<RemoveParticipationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        return ResultSuccess<RemoveParticipationCommand>.CreateSimpleResult(new RemoveParticipationCommand(email, eventId));
    }

    private RemoveParticipationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}