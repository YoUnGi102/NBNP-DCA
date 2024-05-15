using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Guest;

public record RequestJoinResponse(bool Success);

public record RequestJoinRequest([FromBody] Guid GuestId, [FromBody] Guid EventId);

public class RequestJoin(ICommandDispatcher dispatcher)
    : ApiEndpoint.WithRequest<RequestJoinRequest>.WithResponse<RequestJoinResponse>
{
    [HttpPost("guests/join")]
    public override async Task<ActionResult<RequestJoinResponse>> HandleAsync([FromBody] RequestJoinRequest request)
    {
        Result<RequestJoinEventCommand>
            commandResult = RequestJoinEventCommand.Create(request.GuestId, request.EventId);
        if (commandResult is ResultFailure<RequestJoinEventCommand>)
            return Ok(new RequestJoinResponse(false));

        Result<None> result = await dispatcher.DispatchAsync(commandResult.GetObj());

        if (result is ResultSuccess<None>)
        {
            return Ok(new RequestJoinResponse(true));
        }

        return BadRequest(new RequestJoinResponse(false));
    }
}