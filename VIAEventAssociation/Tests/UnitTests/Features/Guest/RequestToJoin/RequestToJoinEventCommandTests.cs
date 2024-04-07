using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace UnitTests.Features.Guest.RequestToJoin;

public class RequestToJoinEventCommandTests
{
    [Fact]
    public void GivenValidData_WhenCreatingCommand_ThenCommandCreated()
    {
        // Arrange
        int validGuestId = 1;
        int validEventId = 1;

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(validGuestId, validEventId);

        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(result.GetObj());
        Assert.Equal(validGuestId, result.GetObj().GuestId);
        Assert.Equal(validEventId, result.GetObj().EventId);
    }

    [Fact]
    public void GivenInvalidData_WhenCreatingCommand_ThenCommandNotCreated()
    {
        // Arrange
        int invalidGuestId = -1;
        int invalidEventId = -1;

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(invalidGuestId, invalidEventId);

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(result.GetObj());
    }
}