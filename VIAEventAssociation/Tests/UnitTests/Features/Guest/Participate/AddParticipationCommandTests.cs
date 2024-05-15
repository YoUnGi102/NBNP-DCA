using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.AddParticipation;

public class AddParticipationCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public AddParticipationCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenAddingParticipation_ThenParticipationAdded()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", id);
        AddParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
        Assert.Equal(id, command.EventId);
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAddingParticipation_ThenParticipationNotAdded()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("", Guid.NewGuid());
        AddParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenAddingParticipation_ThenParticipationNotAdded()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", Guid.Empty);
        AddParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
    }
}