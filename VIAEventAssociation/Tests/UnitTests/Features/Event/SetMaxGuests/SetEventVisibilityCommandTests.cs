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
        Result<SetEventMaxGuestsCommand> result = SetEventMaxGuestsCommand.Create(1, 100);
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
        Result<SetEventMaxGuestsCommand> result = SetEventMaxGuestsCommand.Create(1, -1);
        SetEventMaxGuestsCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}