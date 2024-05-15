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
        _location = new Location("location", 32);
    }

    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsDraft()
    {
        // Arrange
        var creator = new Creator(new Guid(), "creator", "123");
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Draft, new List<Guest>(), _location);
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
        var creator = new Creator(new Guid(), "creator", "123");
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>(), _location);
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
        var creator = new Creator(new Guid(), "creator", "123");
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>(), _location);
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
        var creator = new Creator(new Guid(), "creator", "123");
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Deleted, new List<Guest>(), _location);
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
        var creator = new Creator(new Guid(), "creator", "123");
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Private, EventStatus.Draft, new List<Guest>(), _location);
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
        var creator = new Creator(new Guid(), "creator", "123");
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Draft, new List<Guest>(), _location);
        // Act
        var result = creator.ActivateEvent(_event);
        bool isSuccess = result.GetObj().Status == EventStatus.Active;

        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Active, result.GetObj().Status);
    }
}