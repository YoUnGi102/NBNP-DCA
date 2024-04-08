using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.SendInvitation;

public class SendInvitationCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public SendInvitationCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenSendingInvitation_ThenInvitationSent()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create(1, 1);
        SendInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.GuestId);
        Assert.Equal(1, command.EventId);
    }
    
    [Fact]
    public async Task GivenInvalidGuestId_WhenSendingInvitation_ThenInvitationNotSent()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create(-1, 1);
        SendInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenSendingInvitation_ThenInvitationNotSent()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create(1, -1);
        SendInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}