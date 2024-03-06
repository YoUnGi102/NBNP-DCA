using Xunit;

namespace UnitTests.Features.Event.SetVisibility;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
public class SetEventVisibilityTests
{
    private Event _event;
    
    public SetEventVisibilityTests()
    {
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public, EventStatus.Active, new List<Guest>());
    }
    
    [Fact]
    public void GivenPrivateVisibility_WhenSettingVisibility_ThenVisibilityIsPrivate()
    {
        // Arrange
        var visibility = EventVisibility.Private;
        
        // Act
        _event.SetVisibility(visibility);
        
        // Assert
        Assert.Equal(visibility, _event.GetVisibility());
    }
    
    [Fact]
    public void GivenPublicVisibility_WhenSettingVisibility_ThenVisibilityIsPublic()
    {
        // Arrange
        var visibility = EventVisibility.Public;
        
        // Act
        _event.SetVisibility(visibility);
        
        // Assert
        Assert.Equal(visibility, _event.GetVisibility());
    }
}