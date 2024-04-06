using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventDescriptionCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenUpdatingDescription_ThenDescriptionUpdated()
    {
        // Arrange
        Result<UpdateEventDescriptionCommand> result = UpdateEventDescriptionCommand.Create(1, "New Description");
        UpdateEventDescriptionCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingShortDescription_ThenDescriptionNotUpdated()
    {
        // Arrange
        Result<UpdateEventDescriptionCommand> result = UpdateEventDescriptionCommand.Create(1, "");
        UpdateEventDescriptionCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}