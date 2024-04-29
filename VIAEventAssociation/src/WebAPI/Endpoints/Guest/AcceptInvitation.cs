using Microsoft.AspNetCore.Mvc;
using ObjectMapper;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Guest;

public record AcceptInvitationResponse(bool success);
public record AcceptInvitationRequest([FromBody] string Email, [FromBody] int EventId);

public class AcceptInvitation : ApiEndpoint.WithRequest<AcceptInvitationRequest>.WithResponse<AcceptInvitationResponse>
{
    private readonly ICommandDispatcher _dispatcher;

    public AcceptInvitation(ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("invitation")]
    public override async Task<ActionResult<AcceptInvitationResponse>> HandleAsync([FromBody] AcceptInvitationRequest request)
    {
        AcceptInvitationCommand command = AcceptInvitationCommand.Create(request.Email, request.EventId).GetObj()!;

        // Assuming you dispatch the command here and handle the result accordingly
        var result = await _dispatcher.DispatchAsync(command);

        if (!result.IsFailure())
        {
            return Ok(new AcceptInvitationResponse(true));
        }
        else
        {
            return Ok(new AcceptInvitationResponse(false)); // Or more detailed error handling as necessary
        }
    }
}
