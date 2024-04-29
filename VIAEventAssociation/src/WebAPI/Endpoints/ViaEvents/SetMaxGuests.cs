using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record SetEventMaxGuestsRequest([FromBody] int EventId, [FromBody] int MaxGuests);

public class SetMaxGuests(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<SetEventMaxGuestsRequest>
    .WithResponse<SetEventMaxGuestsResponse>
{
    [HttpPost("events/set-max-guests")]
    
    public override async Task<ActionResult<SetEventMaxGuestsResponse>> HandleAsync([FromBody] SetEventMaxGuestsRequest request)
    {
        SetEventLocationCommand cmd =

        SetEventLocationCommand.Create(request.EventId, request.MaxGuests).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new SetEventLocationResponse(cmd.LocationId.ToString()));
    }
}
public record SetEventMaxGuestsResponse(string Id);