using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.RegisterAccount;

public class RegisterAccountCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public RegisterAccountCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenRegisteringAccount_ThenAccountRegistered()
    {
        // Arrange
        Result<RegisterAccountCommand> result = RegisterAccountCommand.Create("Guest1", "Joe", "Shmoe");
        RegisterAccountCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
    }
    
    [Fact]
    public async Task GivenEmptyUsername_WhenRegisteringAccount_ThenAccountNotRegistered()
    {
        // Arrange
        Result<RegisterAccountCommand> result = RegisterAccountCommand.Create("guest1@gmail.com", "Joe", "Shmoe");
        RegisterAccountCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}