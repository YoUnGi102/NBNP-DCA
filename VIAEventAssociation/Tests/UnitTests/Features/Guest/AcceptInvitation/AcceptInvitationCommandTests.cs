using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.AcceptInvitation;

public class AcceptInvitationCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public AcceptInvitationCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenAcceptingInvitation_ThenInvitationAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        AcceptInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAcceptingInvitation_ThenInvitationNotAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        AcceptInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenAcceptingInvitation_ThenInvitationNotAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", "zzzzzzzzzzzzz");
        AcceptInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}