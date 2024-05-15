using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class AddParticipationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<AddParticipationCommand> Create(string email, Guid eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<AddParticipationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);
        
        return ResultSuccess<AddParticipationCommand>.CreateSimpleResult(new AddParticipationCommand(email, eventId));
    }

    private AddParticipationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}