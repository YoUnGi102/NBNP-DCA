using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;

public class RegisterAccountCommand
{
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public static Result<RegisterAccountCommand> Create(string email, string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName))
            return ResultFailure<RegisterAccountCommand>.CreateMessageResult(null, ["Email cannot be empty."]);

        return ResultSuccess<RegisterAccountCommand>.CreateSimpleResult(new RegisterAccountCommand(email, firstName, lastName));
    }

    private RegisterAccountCommand(string email, string firstName, string lastName)
        => (Email, FirstName, LastName) = (email, firstName, lastName);
}