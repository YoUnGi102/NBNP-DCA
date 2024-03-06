using Domain.Aggregates.Locations;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Guest.Participate;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class ParticipateTests
{
    private ITestOutputHelper _testOutputHelper;
    private Location _location;
    
    public ParticipateTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32, new List<DateTime>([DateTime.Now.AddDays(1)]));
    }
    [Fact]
    public void Participate_WhenEventIsReady_ShouldNotParticipate()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>(), _location);
        var guest = new Guest("email@gmail.com", null, null);
        // Act
        var result = guest.Participate(_event);

        // Assert
        Assert.False(result.GetObj().GetGuests().Contains(guest));
        
    }
    [Fact]
    public void Participate_WhenEventIsFull_ShouldNotParticipate()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 1, EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var guest1 = new Guest("email@gmail.com", null, null);
        var guest2 = new Guest("email2@gmail.com", null, null);
        
        // Act
        guest1.Participate(_event);
        var result = guest2.Participate(_event);
        
        // Assert
        Assert.False(result.GetObj().GetGuests().Contains(guest2));

    }
    
}