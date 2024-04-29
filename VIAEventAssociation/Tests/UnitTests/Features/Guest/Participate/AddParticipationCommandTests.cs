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
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        AddParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Email);
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAddingParticipation_ThenParticipationNotAdded()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        AddParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenAddingParticipation_ThenParticipationNotAdded()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", "");
        AddParticipationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}