using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class AcceptInvitationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<AcceptInvitationCommand> Create(string email, Guid eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<AcceptInvitationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        return ResultSuccess<AcceptInvitationCommand>.CreateSimpleResult(new AcceptInvitationCommand(email, eventId));
    }

    private AcceptInvitationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}