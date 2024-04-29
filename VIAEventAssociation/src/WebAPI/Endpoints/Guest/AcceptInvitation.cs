using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Guest;

public record AcceptInvitationResponse(bool Success);

public record AcceptInvitationRequest([FromBody] string Email, [FromBody] int EventId);

public class AcceptInvitation(ICommandDispatcher dispatcher)
    : ApiEndpoint.WithRequest<AcceptInvitationRequest>.WithResponse<AcceptInvitationResponse>
{
    [HttpPost("guests/invitation/accept")]
    public override async Task<ActionResult<AcceptInvitationResponse>> HandleAsync(
        [FromBody] AcceptInvitationRequest request)
    {
        Result<AcceptInvitationCommand> commandResult = AcceptInvitationCommand.Create(request.Email, request.EventId);
        if (commandResult is ResultFailure<AcceptInvitationCommand>)
            return Ok(new AcceptInvitationResponse(false));

        Result<None> result = await dispatcher.DispatchAsync(commandResult.GetObj());

        if (result is ResultSuccess<None>)
        {
            return Ok(new AcceptInvitationResponse(true));
        }

        return BadRequest(new AcceptInvitationResponse(false));
    }
}