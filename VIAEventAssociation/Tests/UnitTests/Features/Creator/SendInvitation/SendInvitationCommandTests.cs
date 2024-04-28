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
        Result<SendInvitationCommand> result = SendInvitationCommand.Create("e2399bcd-b83b-400f-bfba-2e58cb2b2330", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        SendInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
    }
    
    [Fact]
    public async Task GivenInvalidGuestId_WhenSendingInvitation_ThenInvitationNotSent()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create("", "");
        SendInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenSendingInvitation_ThenInvitationNotSent()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create("", "");
        SendInvitationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}