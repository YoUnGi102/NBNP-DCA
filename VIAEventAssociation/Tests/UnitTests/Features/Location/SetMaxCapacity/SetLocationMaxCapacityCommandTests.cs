using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Location.Update;

public class UpdateLocationMaxCapacityCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenUpdatingMaxCapacity_ThenMaxCapacityUpdated()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> result = UpdateLocationMaxCapacityCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", 100);
        UpdateLocationMaxCapacityCommand command = result.GetObj();

        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }

    [Fact]
    public async Task GivenInvalidData_WhenUpdatingMaxCapacity_ThenMaxCapacityNotUpdated()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> result = UpdateLocationMaxCapacityCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", -1);
        UpdateLocationMaxCapacityCommand command = result.GetObj();

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }

}