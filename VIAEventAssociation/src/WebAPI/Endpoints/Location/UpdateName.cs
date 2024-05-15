using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Location;

public record UpdateLocationNameRequest([FromBody] Guid LocationId ,[FromBody] string Name);

public class UpdateName(ICommandDispatcher dispatcher) : 
    ApiEndpoint
    .WithRequest<UpdateLocationNameRequest>
    .WithResponse<UpdateLocationNameResponse>
{
    [HttpPost("location/update-name")]
    public override async Task<ActionResult<UpdateLocationNameResponse>> HandleAsync(
        [FromBody] UpdateLocationNameRequest request)
    {
        UpdateLocationNameCommand cmd = UpdateLocationNameCommand.Create(request.LocationId ,request.Name).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new UpdateLocationNameResponse(cmd.Id.ToString()));
    }
}

public record UpdateLocationNameResponse(string Id);