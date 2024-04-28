using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class AddParticipationCommand
{
    public string Email { get; }
    public Guid EventId { get; }

    public static Result<AddParticipationCommand> Create(string email, string eventId)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<AddParticipationCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        if (Guid.TryParse(eventId, out Guid eId) && eId != Guid.Empty)
            return ResultFailure<AddParticipationCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

        return ResultSuccess<AddParticipationCommand>.CreateSimpleResult(new AddParticipationCommand(email, eId));
    }

    private AddParticipationCommand(string email, Guid eventId)
        => (Email, EventId) = (email, eventId);
}