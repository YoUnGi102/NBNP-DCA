using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using WebAPI.Endpoints.Common;

namespace WebAPI.Endpoints.Guest;

public record RegisterAccountResponse(bool Success);

public record RegisterAccountRequest([FromBody] string Email);

public class RegisterAccount(ICommandDispatcher dispatcher)
    : ApiEndpoint.WithRequest<RegisterAccountRequest>.WithResponse<RegisterAccountResponse>
{
    [HttpPost("guests/register")]
    public override async Task<ActionResult<RegisterAccountResponse>> HandleAsync(
        [FromBody] RegisterAccountRequest request)
    {
        Result<RegisterAccountCommand> commandResult = RegisterAccountCommand.Create(request.Email);
        if (commandResult is ResultFailure<RegisterAccountCommand>)
            return Ok(new RegisterAccountResponse(false));

        Result<None> result = await dispatcher.DispatchAsync(commandResult.GetObj());

        if (result is ResultSuccess<None>)
        {
            return Ok(new RegisterAccountResponse(true));
        }

        return BadRequest(new RegisterAccountResponse(false));
    }
}