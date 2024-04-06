using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.DeclineInvitation;

public class DeclineInvitationCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DeclineInvitationCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenDecliningInvitation_ThenInvitationDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", 1);
        DeclineInvitationCommand command = result.GetObj();

        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
        Assert.Equal(1, command.EventId);
    }

    [Fact]
    public async Task GivenEmptyEmail_WhenDecliningInvitation_ThenInvitationNotDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("", 1);
        DeclineInvitationCommand command = result.GetObj();

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenDecliningInvitation_ThenInvitationNotDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", -1);
        DeclineInvitationCommand command = result.GetObj();

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}