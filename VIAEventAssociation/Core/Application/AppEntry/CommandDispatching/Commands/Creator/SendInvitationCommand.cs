using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;

public class SendInvitationCommand
{
    public Guid GuestId { get; }
    public Guid EventId { get; }

    public static Result<SendInvitationCommand> Create(string guestId, string eventId)
    {
        Guid gId, eId;
        if (!Guid.TryParse(guestId, out gId) || !Guid.TryParse(eventId, out eId))
        {
            return ResultFailure<SendInvitationCommand>.CreateMessageResult(null, ["EventId and GuestId must be a valid Guid"]);
        }
        return ResultSuccess<SendInvitationCommand>.CreateSimpleResult(new SendInvitationCommand(gId, eId));
    }

    private SendInvitationCommand(Guid guestId, Guid eventId)
        => (GuestId, EventId) = (guestId, eventId);
}