using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class SetEventStatusCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenSettingStatus_ThenStatusSet()
    {
        // Arrange
        Result<SetEventStatusCommand> result = SetEventStatusCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", "Ready");
        SetEventStatusCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingStatus_ThenStatusNotSet()
    {
        // Arrange
        Result<SetEventStatusCommand> result = SetEventStatusCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", "InvalidStatus");
        SetEventStatusCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}