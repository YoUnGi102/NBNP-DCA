using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record UpdateEventStartDateTimeRequest([FromBody] int Id, [FromBody] string StartDateTime);

public class UpdateStartDateTime(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<UpdateEventStartDateTimeRequest>
    .WithResponse<UpdateEventStartDateTimeResponse>
{
    [HttpPost("events/update-start-date-time")]
    public override async Task<ActionResult<UpdateEventStartDateTimeResponse>> HandleAsync(
        [FromBody] UpdateEventStartDateTimeRequest request)
    {
        UpdateEventStartDateTimeCommand cmd = UpdateEventStartDateTimeCommand.Create(request.Id, request.StartDateTime).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new UpdateEventStartDateTimeResponse(cmd.Id.ToString()));
    }
}
public record UpdateEventStartDateTimeResponse(string Id);