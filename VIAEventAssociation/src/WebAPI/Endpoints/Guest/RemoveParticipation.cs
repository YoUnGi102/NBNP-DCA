using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Guest;

public record RemoveParticipationResponse(bool Success);

public record RemoveParticipationRequest([FromBody] string Email, [FromBody] int EventId);

public class RemoveParticipation(ICommandDispatcher dispatcher)
    : ApiEndpoint.WithRequest<RemoveParticipationRequest>.WithResponse<RemoveParticipationResponse>
{
    [HttpPost("guests/participation/remove")]
    public override async Task<ActionResult<RemoveParticipationResponse>> HandleAsync(
        [FromBody] RemoveParticipationRequest request)
    {
        Result<RemoveParticipationCommand> commandResult =
            RemoveParticipationCommand.Create(request.Email, request.EventId);
        if (commandResult is ResultFailure<RemoveParticipationCommand>)
            return Ok(new RemoveParticipationResponse(false));

        Result<None> result = await dispatcher.DispatchAsync(commandResult.GetObj());

        if (result is ResultSuccess<None>)
        {
            return Ok(new RemoveParticipationResponse(true));
        }

        return BadRequest(new RemoveParticipationResponse(false));
    }
}