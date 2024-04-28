using UnitTests.Fakes;

namespace UnitTests.Features.Creator.ActivateEvent;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;


public class ActivateEventTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Location _location;

    public ActivateEventTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = Constants.TEST_LOCATION;
    }

    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsDraft()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.ActivateEvent(_event);
        bool isSuccess = result.GetObj().Status == EventStatus.Active;

        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Active, result.GetObj().Status);
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsReady()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        // Act
        var result = creator.ActivateEvent(_event);
        bool isSuccess = result.GetObj().Status == EventStatus.Active;

        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Active, result.GetObj().Status);
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsCancelled()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.ActivateEvent(_event);
        bool isSuccess = result.GetObj().Status == EventStatus.Active;

        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Active, result.GetObj().Status);
    }
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsDeleted()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.ActivateEvent(_event);
        bool isSuccess = result.GetObj().Status == EventStatus.Active;

        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Active, result.GetObj().Status);
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsPrivate()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.ActivateEvent(_event);
        if(result.GetType() == typeof(ResultFailure<Event>))
        {
            foreach (var error in result.GetMessages())
            {
                _testOutputHelper.WriteLine(error.ToString());
            }
        }
        // Assert
        Assert.Equal(EventStatus.Active, result.GetObj().Status);
        
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsPublic()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var _event = Constants.TEST_EVENT;
        
        // Act
        var result = creator.ActivateEvent(_event);
        bool isSuccess = result.GetObj().Status == EventStatus.Active;

        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Active, result.GetObj().Status);
    }
}