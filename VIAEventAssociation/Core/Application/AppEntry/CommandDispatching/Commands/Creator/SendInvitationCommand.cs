using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;

public class SendInvitationCommand
{
    public Guid GuestId { get; }
    public Guid EventId { get; }

    public static Result<SendInvitationCommand> Create(Guid guestId, Guid eventId)
    {
        return ResultSuccess<SendInvitationCommand>.CreateSimpleResult(new SendInvitationCommand(guestId, eventId));
    }

    private SendInvitationCommand(Guid guestId, Guid eventId)
        => (GuestId, EventId) = (guestId, eventId);
}