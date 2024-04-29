using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventTitleCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenUpdatingTitle_ThenTitleUpdated()
    {
        // Arrange
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", "New Title");
        UpdateEventTitleCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingShortTitle_ThenTitleUpdated()
    {
        // Arrange
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", "");
        UpdateEventTitleCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}