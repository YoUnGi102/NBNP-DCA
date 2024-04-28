using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class DeclineInvitationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<DeclineInvitationCommand> Create(string email, string eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<DeclineInvitationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        if (Guid.TryParse(eventId, out Guid eId) && eId != Guid.Empty)
            return ResultFailure<DeclineInvitationCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

        return ResultSuccess<DeclineInvitationCommand>.CreateSimpleResult(new DeclineInvitationCommand(email, eId));
    }

    private DeclineInvitationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}