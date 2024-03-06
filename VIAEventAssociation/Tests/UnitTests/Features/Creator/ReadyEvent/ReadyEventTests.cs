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
        creator.ReadyEvent(_event);
        
        // Assert
        Assert.NotEqual(EventStatus.Ready, _event.status);
    }
    [Fact]
    public void ReadyEvent_WhenEventIsDeleted_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Deleted, new List<Guest>());

        // Act
        creator.ReadyEvent(_event);
        
        // Assert
        Assert.NotEqual(EventStatus.Ready, _event.status);
    }
    [Fact]
    public void ReadyEvent_WhenEventIsActive_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Active, new List<Guest>());

        // Act
        creator.ReadyEvent(_event);
        
        // Assert
        Assert.NotEqual(EventStatus.Ready, _event.status);
    }
    [Fact]
    public void ReadyEvent_WhenEventIsReady_ShouldReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>());

        // Act
        creator.ReadyEvent(_event);
        
        // Assert
        Assert.Equal(EventStatus.Ready, _event.status);
    }
}