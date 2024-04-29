using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Location;

public record CreateLocationRequest([FromBody] string Name ,[FromBody] int MaxCapacity);

public class Create(ICommandDispatcher dispatcher) :
    ApiEndpoint
    .WithRequest<CreateLocationRequest>
    .WithResponse<CreateLocationResponse>
{
    [HttpPost("location/create")]
    public override async Task<ActionResult<CreateLocationResponse>> HandleAsync(
        [FromBody] CreateLocationRequest request)
    {
        CreateLocationCommand cmd = CreateLocationCommand.Create(request.Name ,request.MaxCapacity).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new CreateLocationResponse(cmd.Name.ToString()));
    }
}
public record CreateLocationResponse(string Name);