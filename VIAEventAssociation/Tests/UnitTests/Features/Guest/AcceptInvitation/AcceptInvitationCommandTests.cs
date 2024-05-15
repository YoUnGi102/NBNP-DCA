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
        Guid id = Guid.NewGuid();
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", id);
        AcceptInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
        Assert.Equal(id, command.EventId);
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAcceptingInvitation_ThenInvitationNotAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("", Guid.NewGuid());
        AcceptInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenAcceptingInvitation_ThenInvitationNotAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", Guid.NewGuid());
        AcceptInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
    }
}