using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class AcceptInvitationCommand
{
    public string Email { get; }
    public int EventId { get; }

    public static Result<AcceptInvitationCommand> Create(string email, int eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<AcceptInvitationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        if (eventId <= 0)
            return ResultFailure<AcceptInvitationCommand>.CreateMessageResult(null, ["EventId must be greater than 0."]);

        return ResultSuccess<AcceptInvitationCommand>.CreateSimpleResult(new AcceptInvitationCommand(email, eventId));
    }

    private AcceptInvitationCommand(string email, int eventId)
        => (Email, EventId) = (email, eventId);
}