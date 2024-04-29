using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record UpdateEventTitleRequest([FromBody] int Id, [FromBody] string Title);

public class UpdateTitle(ICommandDispatcher dispatcher):
    ApiEndpoint
    .WithRequest<UpdateEventTitleRequest>
    .WithResponse<UpdateEventTitleResponse>
{
    [HttpPost("events/update-title")]
    public override async Task<ActionResult<UpdateEventTitleResponse>> HandleAsync([FromBody] UpdateEventTitleRequest request)
    {
        UpdateEventTitleCommand cmd = UpdateEventTitleCommand.Create(request.Id, request.Title).GetObj();
        Result<None> result = await dispatcher.DispatchAsync(cmd);
        return result.IsFailure()
            ? BadRequest(result.GetMessages())
            : Ok(new UpdateEventTitleResponse(cmd.Id.ToString()));
    }
}
public record UpdateEventTitleResponse(string Id);