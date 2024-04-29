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
        string validGuestId = "e2399bcd-b83b-400f-bfba-2e58cb2b2330";
        string validEventId = "3b1d8789-e982-41b4-9f77-a7459fd6f51e";

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(validGuestId, validEventId);

        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(result.GetObj());
        Assert.Equal(validGuestId, result.GetObj().GuestId.ToString());
        Assert.Equal(validEventId, result.GetObj().EventId.ToString());
    }

    [Fact]
    public void GivenInvalidData_WhenCreatingCommand_ThenCommandNotCreated()
    {
        // Arrange
        string invalidGuestId = "";
        string invalidEventId = "";

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(invalidGuestId, invalidEventId);

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(result.GetObj());
    }
}