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
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        DeclineInvitationCommand command = result.GetObj();

        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
    }

    [Fact]
    public async Task GivenEmptyEmail_WhenDecliningInvitation_ThenInvitationNotDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        DeclineInvitationCommand command = result.GetObj();

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenDecliningInvitation_ThenInvitationNotDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", "");
        DeclineInvitationCommand command = result.GetObj();

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}