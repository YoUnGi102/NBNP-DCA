using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.ViaEvents;

public record CreateEventRequest([FromBody] string Title, [FromBody] string Description, [FromBody] string StartDate, 
    [FromBody] string EndDate, [FromBody] int MaxGuests, [FromBody] string Visibility, 
    [FromBody] string Status, [FromBody] Guid LocationId);

    public class Create(ICommandDispatcher dispatcher) :
        ApiEndpoint
        .WithRequest<CreateEventRequest>
        .WithResponse<CreateEventResponse>
    {
        [HttpPost("events/create")]

        public override async Task<ActionResult<CreateEventResponse>> HandleAsync([FromBody] CreateEventRequest request)
        {
            CreateEventCommand cmd = CreateEventCommand.Create(request.Title, request.Description, 
                request.StartDate, request.EndDate, request.MaxGuests, 
                request.Visibility, request.Status, request.LocationId).GetObj();
            Result<None> result = await dispatcher.DispatchAsync(cmd);
            return result.IsFailure()
                ? BadRequest(result.GetMessages())
                : Ok(new CreateEventResponse(cmd.Title.ToString()));
        }
    }
public record CreateEventResponse(string Id);