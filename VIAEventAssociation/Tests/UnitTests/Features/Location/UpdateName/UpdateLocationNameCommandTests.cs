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
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create(Guid.NewGuid(), "New Name");
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
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create(Guid.NewGuid(), "");
        UpdateLocationNameCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}