using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RemoveParticipationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<RemoveParticipationCommand> Create(string email, string eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<RemoveParticipationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        if (Guid.TryParse(eventId, out Guid eId) && eId != Guid.Empty)
            return ResultFailure<RemoveParticipationCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

        return ResultSuccess<RemoveParticipationCommand>.CreateSimpleResult(new RemoveParticipationCommand(email, eId));
    }

    private RemoveParticipationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}