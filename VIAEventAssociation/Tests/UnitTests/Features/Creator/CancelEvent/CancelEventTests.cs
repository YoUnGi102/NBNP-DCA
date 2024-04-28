using UnitTests.Fakes;

namespace UnitTests.Features.Creator.CancelEvent;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class CancelEventTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Location _location;
    
    public CancelEventTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32);
    }
    [Fact]
    public void CancelEvent_WhenEventIsReady_ShouldCancelEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.CancelEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.Equal(EventStatus.Cancelled, result.GetObj().Status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsActive_ShouldCancelEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;

        // Act
        var result = creator.CancelEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        // Assert
        Assert.Equal(EventStatus.Cancelled, result.GetObj().Status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsDeleted_ShouldNotCancelEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.CancelEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.NotEqual(EventStatus.Cancelled, result.GetObj().Status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsCancelled_ShouldNotCancelEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.CancelEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(EventStatus.Cancelled, result.GetObj().Status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsDraft_ShouldNotCancelEvent()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.CancelEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.NotEqual(EventStatus.Cancelled, result.GetObj().Status);
    }
    
    [Fact]
    public void CancelEvent_WhenEventIsCancelled_ShouldNotChangeEventStatus()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.CancelEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        
        // Assert
        Assert.Equal(EventStatus.Cancelled, result.GetObj().Status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsDeleted_ShouldNotChangeEventStatus()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.CancelEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.Equal(EventStatus.Deleted, result.GetObj().Status);
    }
}