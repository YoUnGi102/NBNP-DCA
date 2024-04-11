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
        Result<SetEventLocationCommand> result = SetEventLocationCommand.Create(1, 1);
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
        Result<SetEventLocationCommand> result = SetEventLocationCommand.Create(1, -1);
        SetEventLocationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}