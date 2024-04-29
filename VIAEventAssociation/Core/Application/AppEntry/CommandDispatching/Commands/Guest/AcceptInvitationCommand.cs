using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class AcceptInvitationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<AcceptInvitationCommand> Create(string email, string eventId)
    {
        if (Guid.TryParse(eventId, out Guid eId))
            return ResultFailure<AcceptInvitationCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);
        
        if (string.IsNullOrEmpty(email))
            return ResultFailure<AcceptInvitationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);
        
        return ResultSuccess<AcceptInvitationCommand>.CreateSimpleResult(new AcceptInvitationCommand(email, eId));
    }

    private AcceptInvitationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}