using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Creator;


public record SendInvitationResponse(bool Success);

public record SendInvitationRequest([FromBody] Guid GuestID, [FromBody] Guid EventId);

public class SendInvitation(ICommandDispatcher dispatcher): ApiEndpoint.WithRequest<SendInvitationRequest>.WithResponse<SendInvitationResponse>
{
    [HttpPost("creator/invitation/send")]
    public override async Task<ActionResult<SendInvitationResponse>> HandleAsync([FromBody] SendInvitationRequest request)
    {
        Result<SendInvitationCommand> commandResult = SendInvitationCommand.Create(request.GuestID, request.EventId);
        if (commandResult is ResultFailure<SendInvitationCommand>)
            return Ok(new SendInvitationResponse(false));

        Result<None> result = await dispatcher.DispatchAsync(commandResult.GetObj());

        if (result is ResultSuccess<None>)
        {
            return Ok(new SendInvitationResponse(true));
        }

        return BadRequest(new SendInvitationResponse(false));
    }
}