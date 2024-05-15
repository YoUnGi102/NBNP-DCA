using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Location;

public record AddLocationAvailabilityIntervalRequest([FromBody] Guid LocationId ,[FromBody] string Start ,[FromBody] string End);

public class AddAvailabilityInterval(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<AddLocationAvailabilityIntervalRequest>
    .WithResponse<AddLocationAvailabilityIntervalResponse>
{
    [HttpPost("location/add-availability-interval")]
    public override async Task<ActionResult<AddLocationAvailabilityIntervalResponse>> HandleAsync(
        [FromBody] AddLocationAvailabilityIntervalRequest request)
    {
        AddAvailabilityIntervalCommand cmd = AddAvailabilityIntervalCommand.Create(request.LocationId ,request.Start ,request.End).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new AddLocationAvailabilityIntervalResponse(cmd.LocationId.ToString()));
    }
}
public record AddLocationAvailabilityIntervalResponse(string Id);