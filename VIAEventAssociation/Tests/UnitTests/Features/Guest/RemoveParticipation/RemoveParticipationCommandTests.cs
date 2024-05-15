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
        Guid id = Guid.NewGuid();
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("guest1@gmail.com", id);
        RemoveParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
        Assert.Equal(id, command.EventId);
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenRemovingParticipation_ThenParticipationNotRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("", Guid.NewGuid());
        RemoveParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenRemovingParticipation_ThenParticipationNotRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("Guest1@example.com", Guid.Empty);
        RemoveParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
    }
}