using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RegisterAccountCommand
{
    public string Email { get; }

    public static Result<RegisterAccountCommand> Create(string email)
    {
        if (string.IsNullOrEmpty(email))
            return ResultFailure<RegisterAccountCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        return ResultSuccess<RegisterAccountCommand>.CreateSimpleResult(new RegisterAccountCommand(email));
    }

    private RegisterAccountCommand(string email)
        => (Email) = (email);
}