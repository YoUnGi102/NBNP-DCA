using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class CreateEventCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public CreateEventCommandTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GivenValidData_WhenCreatingEvent_ThenEventCreated()
    {
        // Arrange
        Result<CreateEventCommand> result = CreateEventCommand.Create(
            "Chess Tournament", 
            "Monthly Chess Tournament by VIA Chess Club", 
            Constants.START_DATE_STRING,
            Constants.END_DATE_STRING, 
            100, 
            "Public",
            "Active", 
            1);
        CreateEventCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Title);
    }
    
    [Fact]
    public async Task GivenShortTitle_WhenCreatingEvent_ThenEventCreated()
    {
        // Arrange
        Result<CreateEventCommand> result = CreateEventCommand.Create(
            "", 
            "Monthly Chess Tournament by VIA Chess Club", 
            Constants.START_DATE_STRING,
            Constants.END_DATE_STRING, 
            100, 
            "Visible",
            "Active", 
            1);
        CreateEventCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}