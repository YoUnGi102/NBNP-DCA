using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class DeclineInvitationCommand
{
    public string Email { get; }
    public int EventId { get; }

    public static Result<DeclineInvitationCommand> Create(string email, int eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<DeclineInvitationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        if (eventId <= 0)
            return ResultFailure<DeclineInvitationCommand>.CreateMessageResult(null, ["EventId must be greater than 0."]);

        return ResultSuccess<DeclineInvitationCommand>.CreateSimpleResult(new DeclineInvitationCommand(email, eventId));
    }

    private DeclineInvitationCommand(string email, int eventId)
        => (Email, EventId) = (email, eventId);
}