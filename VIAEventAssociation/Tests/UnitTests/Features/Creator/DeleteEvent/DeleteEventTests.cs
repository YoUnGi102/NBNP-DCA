using Xunit;

namespace UnitTests.Features.Creator.DeleteEvent;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;
public class DeleteEventTests
{
    [Fact]
    public void DeleteEvent_WhenEventIsReady_ShouldDeleteEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>());

        // Act
        creator.DeleteEvent(_event);
        
        // Assert
        Assert.Equal(EventStatus.Deleted, _event.Status);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsActive_ShouldDeleteEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Active, new List<Guest>());

        // Act
        creator.DeleteEvent(_event);
        
        // Assert
        Assert.Equal(EventStatus.Deleted, _event.Status);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsDeleted_ShouldNotDeleteEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Deleted, new List<Guest>());

        // Act
        creator.DeleteEvent(_event);
        
        // Assert
        Assert.NotEqual(EventStatus.Deleted, _event.Status);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsCancelled_ShouldNotDeleteEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>());

        // Act
        creator.DeleteEvent(_event);
        
        // Assert
        Assert.NotEqual(EventStatus.Deleted, _event.Status);
    }
    
}