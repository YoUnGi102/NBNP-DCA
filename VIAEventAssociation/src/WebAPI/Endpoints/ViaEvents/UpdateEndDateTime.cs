using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record UpdateEventEndDateTimeRequest([FromBody] Guid Id, [FromBody] string EndDateTime);

public class UpdateEndDateTime(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<UpdateEventEndDateTimeRequest>
    .WithResponse<UpdateEventEndDateTimeResponse>
{
    [HttpPost("events/update-end-date-time")]
    public override async Task<ActionResult<UpdateEventEndDateTimeResponse>> HandleAsync(
        [FromBody] UpdateEventEndDateTimeRequest request)
    {
        UpdateEventEndDateTimeCommand cmd = UpdateEventEndDateTimeCommand.Create(request.Id, request.EndDateTime).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new UpdateEventEndDateTimeResponse(cmd.Id.ToString()));
    }
}
public record UpdateEventEndDateTimeResponse(string Id);