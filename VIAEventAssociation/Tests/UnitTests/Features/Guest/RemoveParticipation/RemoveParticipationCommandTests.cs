using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.RemoveParticipation;

public class RemoveParticipationCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public RemoveParticipationCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenRemovingParticipation_ThenParticipationRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("guest1@gmail.com", 1);
        RemoveParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
        Assert.Equal(1, command.EventId);
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenRemovingParticipation_ThenParticipationNotRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("", 1);
        RemoveParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenRemovingParticipation_ThenParticipationNotRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("Guest1@example.com", -1);
        RemoveParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}