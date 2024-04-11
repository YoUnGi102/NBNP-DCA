using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class SetEventVisibilityCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenSettingVisibility_ThenVisibilitySet()
    {
        // Arrange
        Result<SetEventVisibilityCommand> result = SetEventVisibilityCommand.Create(1, "Public");
        SetEventVisibilityCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenValidData2_WhenSettingVisibility_ThenVisibilitySet()
    {
        // Arrange
        Result<SetEventVisibilityCommand> result = SetEventVisibilityCommand.Create(1, "Private");
        SetEventVisibilityCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingVisibility_ThenVisibilityNotSet()
    {
        // Arrange
        Result<SetEventVisibilityCommand> result = SetEventVisibilityCommand.Create(1, "InvalidVisibility");
        SetEventVisibilityCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}