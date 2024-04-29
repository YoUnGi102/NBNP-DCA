using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class SetEventMaxGuestsCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenSettingMaxGuests_ThenMaxGuestsSet()
    {
        // Arrange
        Result<SetEventMaxGuestsCommand> result = SetEventMaxGuestsCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", 100);
        SetEventMaxGuestsCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingMaxGuests_ThenMaxGuestsNotSet()
    {
        // Arrange
        Result<SetEventMaxGuestsCommand> result = SetEventMaxGuestsCommand.Create("", -1);
        SetEventMaxGuestsCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}