using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Location.Update;

public class UpdateLocationNameCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenUpdatingName_ThenNameUpdated()
    {
        // Arrange
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", "New Name");
        UpdateLocationNameCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingShortName_ThenNameNotUpdated()
    {
        // Arrange
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", "");
        UpdateLocationNameCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}