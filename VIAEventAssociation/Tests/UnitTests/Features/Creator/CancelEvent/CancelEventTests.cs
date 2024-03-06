using Xunit;

namespace UnitTests.Features.Creator.CancelEvent;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class CancelEventTests
{
    [Fact]
    public void CancelEvent_WhenEventIsReady_ShouldCancelEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>());

        // Act
        var result = creator.CancelEvent(_event);
        bool isSuccess = result.GetObj().status == EventStatus.Cancelled;

        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Cancelled, result.GetObj().status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsActive_ShouldCancelEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Active, new List<Guest>());

        // Act
        var result = creator.CancelEvent(_event);
        bool isSuccess = result.GetObj().status == EventStatus.Cancelled;
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Cancelled, result.GetObj().status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsDeleted_ShouldNotCancelEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Deleted, new List<Guest>());

        // Act
        var result = creator.CancelEvent(_event);
        bool isSuccess = result.GetObj().status != EventStatus.Cancelled;
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotEqual(EventStatus.Cancelled, result.GetObj().status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsCancelled_ShouldNotCancelEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>());

        // Act
        var result = creator.CancelEvent(_event);
        bool isSuccess = result.GetObj().status != EventStatus.Cancelled;

        // Assert
        Assert.True(isSuccess);
        Assert.NotEqual(EventStatus.Cancelled, result.GetObj().status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsDraft_ShouldNotCancelEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Draft, new List<Guest>());

        // Act
        var result = creator.CancelEvent(_event);
        bool isSuccess = result.GetObj().status != EventStatus.Cancelled;
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotEqual(EventStatus.Cancelled, result.GetObj().status);
    }
    
    [Fact]
    public void CancelEvent_WhenEventIsCancelled_ShouldNotChangeEventStatus()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>());

        // Act
        var result = creator.CancelEvent(_event);
        bool isSuccess = result.GetObj().status == EventStatus.Cancelled;

        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Cancelled, result.GetObj().status);
    }
    [Fact]
    public void CancelEvent_WhenEventIsDeleted_ShouldNotChangeEventStatus()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Deleted, new List<Guest>());

        // Act
        var result = creator.CancelEvent(_event);
        bool isSuccess = result.GetObj().status == EventStatus.Cancelled;
        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Cancelled, result.GetObj().status);
    }
}