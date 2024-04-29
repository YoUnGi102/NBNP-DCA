using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record UpdateEventDescriptionRequest([FromBody] int Id ,[FromBody] string Description);

public class UpdateDescription(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<UpdateEventDescriptionRequest>
    .WithResponse<UpdateEventDescriptionResponse>
{
    [HttpPost("events/update-description")]
    public override async Task<ActionResult<UpdateEventDescriptionResponse>> HandleAsync(
        [FromBody] UpdateEventDescriptionRequest request)
    {
        UpdateEventDescriptionCommand cmd = UpdateEventDescriptionCommand.Create(request.Id ,request.Description).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new UpdateEventDescriptionResponse(cmd.Id.ToString()));
    }
}
public record UpdateEventDescriptionResponse(string Id);