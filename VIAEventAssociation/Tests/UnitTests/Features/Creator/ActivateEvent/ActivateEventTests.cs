using Xunit;

namespace UnitTests.Features.Creator.ActivateEvent;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;


public class ActivateEventTests
{
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsDraft()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Draft, new List<Guest>());
        // Act
        creator.ActiveEvent(_event);
        var result = false;
        if(_event.Status == EventStatus.Active)
        {
            result = true;
        }

        // Assert
        Assert.True(result);
        Assert.Equal(EventStatus.Active, _event.Status);
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsReady()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>());
        // Act
        creator.ActiveEvent(_event);
        var result = false;
        if(_event.Status == EventStatus.Active)
        {
            result = true;
        }

        // Assert
        Assert.True(result);
        Assert.Equal(EventStatus.Active, _event.Status);
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsCancelled()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>());
        // Act
        creator.ActiveEvent(_event);
        var result = false;
        if(_event.Status == EventStatus.Active)
        {
            result = true;
        }

        // Assert
        Assert.True(result);
        Assert.Equal(EventStatus.Active, _event.Status);
    }
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsDeleted()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Deleted, new List<Guest>());
        // Act
        creator.ActiveEvent(_event);
        var result = false;
        if(_event.Status == EventStatus.Active)
        {
            result = true;
        }

        // Assert
        Assert.True(result);
        Assert.Equal(EventStatus.Active, _event.Status);
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsPrivate()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Private, EventStatus.Draft, new List<Guest>());
        // Act
        creator.ActiveEvent(_event);
        var result = false;
        if(_event.Status == EventStatus.Active)
        {
            result = true;
        }

        // Assert
        Assert.True(result);
        Assert.Equal(EventStatus.Active, _event.Status);
    }
    
    [Fact]
    public void ChangeEventStatusToActive_WhenEventIsPublic()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Draft, new List<Guest>());
        // Act
        creator.ActiveEvent(_event);
        var result = false;
        if(_event.Status == EventStatus.Active)
        {
            result = true;
        }

        // Assert
        Assert.True(result);
        Assert.Equal(EventStatus.Active, _event.Status);
    }
}