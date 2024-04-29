using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record SetEventVisibilityRequest([FromBody] int Id, [FromBody] string IsVisible);

public class SetVisibility(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<SetEventVisibilityRequest>
    .WithResponse<SetEventVisibilityResponse>
{
    [HttpPost("events/set-visibility")]
    public override async Task<ActionResult<SetEventVisibilityResponse>> HandleAsync([FromBody] SetEventVisibilityRequest request)
    {
        SetEventVisibilityCommand cmd = SetEventVisibilityCommand.Create(request.Id, request.IsVisible).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new SetEventVisibilityResponse(cmd.Id.ToString()));
    }
}
public record SetEventVisibilityResponse(string Id);