using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class DeclineInvitationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<DeclineInvitationCommand> Create(string email, Guid eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<DeclineInvitationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        return ResultSuccess<DeclineInvitationCommand>.CreateSimpleResult(new DeclineInvitationCommand(email, eventId));
    }

    private DeclineInvitationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}