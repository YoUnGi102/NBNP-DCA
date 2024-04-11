using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;

public class SendInvitationCommand
{
    public int GuestId { get; }
    public int EventId { get; }

    public static Result<SendInvitationCommand> Create(int guestId, int eventId)
    {
    
        if(guestId <= 0)
            return ResultFailure<SendInvitationCommand>.CreateMessageResult(null, ["GuestId must be greater than 0."]);
        
        if (eventId <= 0)
            return ResultFailure<SendInvitationCommand>.CreateMessageResult(null, ["EventId must be greater than 0."]);

        return ResultSuccess<SendInvitationCommand>.CreateSimpleResult(new SendInvitationCommand(guestId, eventId));
    }

    private SendInvitationCommand(int guestId, int eventId)
        => (GuestId, EventId) = (guestId, eventId);
}