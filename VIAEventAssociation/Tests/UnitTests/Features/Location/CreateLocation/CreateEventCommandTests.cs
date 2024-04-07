using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Location.Create;

public class CreateLocationCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public CreateLocationCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenCreatingLocation_ThenLocationCreated()
    {
        // Arrange
        Result<CreateLocationCommand> result = CreateLocationCommand.Create("Chess Club", 100);
        CreateLocationCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Name);
    }
    
    [Fact]
    public async Task GivenShortName_WhenCreatingLocation_ThenLocationNotCreated()
    {
        // Arrange
        Result<CreateLocationCommand> result = CreateLocationCommand.Create("", 100);
        CreateLocationCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}