using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Location;

public record UpdateLocationMaxCapacityRequest([FromBody] int LocationId ,[FromBody] int MaxCapacity);

public class UpdateMaxCapacity(ICommandDispatcher dispatcher) : 
    ApiEndpoint
    .WithRequest<UpdateLocationMaxCapacityRequest>
    .WithResponse<UpdateLocationMaxCapacityResponse>
{
    [HttpPost("location/update-max-capacity")]
    public override async Task<ActionResult<UpdateLocationMaxCapacityResponse>> HandleAsync(
        [FromBody] UpdateLocationMaxCapacityRequest request)
    {
        UpdateLocationMaxCapacityCommand cmd = UpdateLocationMaxCapacityCommand.Create(request.LocationId ,request.MaxCapacity).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new UpdateLocationMaxCapacityResponse(cmd.Id.ToString()));
    }
}
public record UpdateLocationMaxCapacityResponse(string Id);