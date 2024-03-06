using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;
using Domain.Aggregates.Events;
public class UpdateEventTitleAgregateTests
{
    private Event _event;
    
    public UpdateEventTitleAgregateTests()
    {
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public, EventStatus.Active, new List<Guest>());
    }
    [Fact]
    public void GivenGoodTitle_WhenUpdatingTitle_ThenTitleIsUpdated()
    {
        // Arrange
        var title = "New Title";
        
        // Act
        _event.UpdateTitle(title);
        
        // Assert
        Assert.Equal(title, _event.title);
    }
    
    [Fact]
    public void GivenNoTitle_WhenUpdatingTitle_ThenTitleIsNotUpdated()
    {
        // Arrange
        var title = "";
        
        // Act
        _event.UpdateTitle(title);
        
        // Assert
        Assert.NotEqual(title, _event.title);
    }
    
    [Fact]
    public void GivenTooLongTitle_WhenUpdatingTitle_ThenTitleIsNotUpdated()
    {
        // Arrange
        string title = new string('a', 101);
        
        // Act
        _event.UpdateTitle(title);
        
        // Assert
        Assert.NotEqual(title, _event.title);
    }
    
    [Fact]
    public void GivenTooShortTitle_WhenUpdatingTitle_ThenTitleIsNotUpdated()
    {
        // Arrange
        string title = new string('a', 1);
        
        // Act
        _event.UpdateTitle(title);
        
        // Assert
        Assert.NotEqual(title, _event.title);
    }
}