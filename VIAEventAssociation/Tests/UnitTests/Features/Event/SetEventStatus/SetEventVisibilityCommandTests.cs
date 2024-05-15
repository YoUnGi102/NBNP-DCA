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
        Result<SetEventStatusCommand> result = SetEventStatusCommand.Create(new Guid(), "Ready");
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
        Result<SetEventStatusCommand> result = SetEventStatusCommand.Create(new Guid(), "InvalidStatus");
        SetEventStatusCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}