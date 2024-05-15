using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record SetEventStatusRequest([FromBody] Guid EventId, [FromBody] string Status);

public class SetStatus(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<SetEventStatusRequest>
    .WithResponse<SetEventStatusResponse>
{
    [HttpPost("events/set-status")]
    
    public override async Task<ActionResult<SetEventStatusResponse>> HandleAsync([FromBody] SetEventStatusRequest request)
    {
        SetEventStatusCommand cmd =

            SetEventStatusCommand.Create(request.EventId, request.Status).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new SetEventLocationResponse(cmd.Id.ToString()));
    }
}

public record SetEventStatusResponse(string id);