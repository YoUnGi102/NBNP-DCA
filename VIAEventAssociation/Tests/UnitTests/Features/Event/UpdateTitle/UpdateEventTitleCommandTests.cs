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
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(1, "New Title");
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
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(1, "");
        UpdateEventTitleCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}