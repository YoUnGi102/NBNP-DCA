using UnitTests.Fakes;

namespace UnitTests.Features.Creator.DeleteEvent;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;
public class DeleteEventTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Location _location;
    
    public DeleteEventTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsReady_ShouldDeleteEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.DeleteEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.Equal(EventStatus.Deleted, result.GetObj().Status);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsActive_ShouldDeleteEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.DeleteEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.Equal(EventStatus.Deleted, result.GetObj().Status);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsCancelled_ShouldNotDeleteEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.DeleteEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.NotEqual(EventStatus.Deleted, result.GetObj().Status);
    }
    
}