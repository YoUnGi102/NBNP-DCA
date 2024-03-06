using Xunit;

namespace UnitTests.Features.Creator.ReadyEvent;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;
public class ReadyEventTests
{
    [Fact]
    public void ReadyEvent_WhenEventIsCancelled_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>());

        // Act
        var result = creator.ReadyEvent(_event);
        bool isSuccess = result.GetObj().status != EventStatus.Ready;
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotEqual(EventStatus.Ready, result.GetObj().status);
    }
    [Fact]
    public void ReadyEvent_WhenEventIsDeleted_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Deleted, new List<Guest>());

        // Act
        var result = creator.ReadyEvent(_event);
        bool isSuccess = result.GetObj().status != EventStatus.Ready;
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotEqual(EventStatus.Ready, result.GetObj().status);
    }
    [Fact]
    public void ReadyEvent_WhenEventIsActive_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Active, new List<Guest>());

        // Act
        var result = creator.ReadyEvent(_event);
        bool isSuccess = result.GetObj().status != EventStatus.Ready;
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotEqual(EventStatus.Ready, result.GetObj().status);
    }
    [Fact]
    public void ReadyEvent_WhenEventIsReady_ShouldReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>());

        // Act
        var result = creator.ReadyEvent(_event);
        bool isSuccess = result.GetObj().status == EventStatus.Ready;
        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Ready, result.GetObj().status);
    }
}