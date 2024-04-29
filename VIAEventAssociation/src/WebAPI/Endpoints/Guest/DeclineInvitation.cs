using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Guest;

public record DeclineInvitationResponse(bool Success);

public record DeclineInvitationRequest([FromBody] string Email, [FromBody] int EventId);

public class DeclineInvitation(ICommandDispatcher dispatcher)
    : ApiEndpoint.WithRequest<DeclineInvitationRequest>.WithResponse<DeclineInvitationResponse>
{
    [HttpPost("guests/invitation/decline")]
    public override async Task<ActionResult<DeclineInvitationResponse>> HandleAsync(
        [FromBody] DeclineInvitationRequest request)
    {
        Result<DeclineInvitationCommand>
            commandResult = DeclineInvitationCommand.Create(request.Email, request.EventId);
        if (commandResult is ResultFailure<DeclineInvitationCommand>)
            return Ok(new DeclineInvitationResponse(false));

        Result<None> result = await dispatcher.DispatchAsync(commandResult.GetObj());

        if (result is ResultSuccess<None>)
        {
            return Ok(new DeclineInvitationResponse(true));
        }

        return BadRequest(new DeclineInvitationResponse(false));
    }
}