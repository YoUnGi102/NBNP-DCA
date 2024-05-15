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
        Guid validGuestId = Guid.NewGuid();
        Guid validEventId = Guid.NewGuid();

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
        Guid invalidGuestId = Guid.Empty;
        Guid invalidEventId = Guid.Empty;

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(invalidGuestId, invalidEventId);

        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(result.GetObj());
    }
}