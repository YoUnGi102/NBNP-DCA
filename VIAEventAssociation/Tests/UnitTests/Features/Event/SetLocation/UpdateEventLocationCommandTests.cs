using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class SetEventLocationCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenSettingLocation_ThenLocationSet()
    {
        // Arrange
        Result<SetEventLocationCommand> result = SetEventLocationCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", "7c59adac-5a10-4de9-8783-ea2add07bb65");
        SetEventLocationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.EventId.ToString());
        Assert.NotNull(command.LocationId.ToString());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingLocation_ThenLocationNotSet()
    {
        // Arrange
        Result<SetEventLocationCommand> result = SetEventLocationCommand.Create("", "");
        SetEventLocationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}