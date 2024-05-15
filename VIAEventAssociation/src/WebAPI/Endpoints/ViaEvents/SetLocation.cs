using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record SetEventLocationRequest([FromBody] Guid EventId, [FromBody] Guid LocationId);

public class SetLocation(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<SetEventLocationRequest>
    .WithResponse<SetEventLocationResponse>
{
    [HttpPost("events/set-location")]
    public override async Task<ActionResult<SetEventLocationResponse>> HandleAsync([FromBody] SetEventLocationRequest request)
    {
        
    
        SetEventLocationCommand cmd =

        SetEventLocationCommand.Create(request.EventId, request.LocationId).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new SetEventLocationResponse(cmd.LocationId.ToString()));
    }
}

public record SetEventLocationResponse(string Id);