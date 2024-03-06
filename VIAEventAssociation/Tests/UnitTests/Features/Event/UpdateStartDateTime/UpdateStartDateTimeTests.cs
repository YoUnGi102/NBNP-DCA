using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit;

namespace UnitTests.Features.Event.UpdateStartDateTime;
using Domain.Aggregates.Events;
public class UpdateStartDateTimeTests
{
    private Event _event;
    
    public UpdateStartDateTimeTests()
    {
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public, EventStatus.Active, new List<Guest>());
    }
    
    [Fact]
    public void GivenGoodStartDateTime_WhenUpdatingStartDateTime_ThenStartDateTimeIsUpdated()
    {
        // Arrange
        var startDateTime = DateTime.Now;
        
        // Act
        _event.UpdateStartDateTime(startDateTime);
        
        // Assert
        Assert.Equal(startDateTime, _event.GetStartDateTime());
    }

    [Fact]
    public void GivenBadStartDateTime_WhenUpdatingStartDateTime_ThenStartDateTimeIsNotUpdated()
    {
        // Arrange
        var startDateTime = DateTime.MinValue;

        // Act
        _event.UpdateStartDateTime(startDateTime);

        // Assert
        Assert.NotEqual(startDateTime, _event.GetStartDateTime());
    }
}