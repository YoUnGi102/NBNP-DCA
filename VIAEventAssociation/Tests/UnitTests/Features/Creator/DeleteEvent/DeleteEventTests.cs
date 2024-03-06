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
        var result = creator.DeleteEvent(_event);
        bool isSuccess = result.GetObj().status == EventStatus.Deleted;
        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Deleted, result.GetObj().status);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsActive_ShouldDeleteEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Active, new List<Guest>());

        // Act
        var result = creator.DeleteEvent(_event);
        bool isSuccess = result.GetObj().status == EventStatus.Deleted;
        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(EventStatus.Deleted, result.GetObj().status);
    }
    [Fact]
    public void DeleteEvent_WhenEventIsCancelled_ShouldNotDeleteEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>());

        // Act
        var result = creator.DeleteEvent(_event);
        bool isSuccess = result.GetObj().status != EventStatus.Deleted;
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotEqual(EventStatus.Deleted, result.GetObj().status);
    }
    
}