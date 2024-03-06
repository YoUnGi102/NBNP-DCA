using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit;

namespace UnitTests.Features.Event.UpdateDescription;
using Domain.Aggregates.Events;
public class UpdateEventDescriptionAggregateTests
{
    private Event _event;
    
    public UpdateEventDescriptionAggregateTests()
    {
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public, EventStatus.Active, new List<Guest>());
    }
    
    [Fact]
    public void GivenGoodDescription_WhenUpdatingDescription_ThenDescriptionIsUpdated()
    {
        // Arrange
        var description = "New Description";
        
        // Act
        _event.UpdateDescription(description);
        
        // Assert
        Assert.Equal(description, _event.GetDescription());
    }
    
    [Fact]
    public void GivenNoDescription_WhenUpdatingDescription_ThenDescriptionIsNotUpdated()
    {
        // Arrange
        var description = "";
        
        // Act
        _event.UpdateDescription(description);
        
        // Assert
        Assert.NotEqual(description, _event.GetDescription());
    } 
    
    [Fact]
    public void GivenTooLongDescription_WhenUpdatingDescription_ThenDescriptionIsNotUpdated()
    {
        // Arrange
        string description = new string('a', 1001);
        
        // Act
        _event.UpdateDescription(description);
        
        // Assert
        Assert.NotEqual(description, _event.GetDescription());
    }
    
    [Fact]
    public void GivenTooShortDescription_WhenUpdatingDescription_ThenDescriptionIsNotUpdated()
    {
        // Arrange
        string description = new string('a', 10);
        
        // Act
        _event.UpdateDescription(description);
        
        // Assert
        Assert.NotEqual(description, _event.GetDescription());
    }
}