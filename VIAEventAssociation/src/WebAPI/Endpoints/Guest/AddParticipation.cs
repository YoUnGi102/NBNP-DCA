using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Guest;

public record AddParticipationResponse(bool Success);

public record AddParticipationRequest([FromBody] string Email, [FromBody] int EventId);

public class AddParticipation(ICommandDispatcher dispatcher)
    : ApiEndpoint.WithRequest<AddParticipationRequest>.WithResponse<AddParticipationResponse>
{
    [HttpPost("guests/participation")]
    public override async Task<ActionResult<AddParticipationResponse>> HandleAsync(
        [FromBody] AddParticipationRequest request)
    {
        Result<AddParticipationCommand> commandResult = AddParticipationCommand.Create(request.Email, request.EventId);
        if (commandResult is ResultFailure<AddParticipationCommand>)
            return Ok(new AddParticipationResponse(false));

        Result<None> result = await dispatcher.DispatchAsync(commandResult.GetObj());

        if (result is ResultSuccess<None>)
        {
            return Ok(new AddParticipationResponse(true));
        }

        return BadRequest(new AddParticipationResponse(false));
    }
}