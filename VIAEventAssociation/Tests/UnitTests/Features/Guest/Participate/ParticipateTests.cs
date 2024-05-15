namespace UnitTests.Features.Guest.Participate;

using Domain.Aggregates.Locations;
using Xunit;
using Xunit.Abstractions;
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
        Location location = new Location("location", 32);

    }
    [Fact]
    public void Participate_WhenEventIsReady_ShouldNotParticipate()
    {
        // Arrange
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Ready, new List<Guest>(), _location);
        var guest = new Guest("email@gmail.com");
        // Act
        var result = guest.Participate(_event);

        // Assert
        Assert.False(_event.Guests.Contains(guest));
        
    }
    [Fact]
    public void Participate_WhenEventIsFull_ShouldNotParticipate()
    {
        // Arrange
        var _event = new Event(new Guid(), "event", "description", DateTime.Now, DateTime.Now, 1, EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var guest1 = new Guest("email@gmail.com");
        var guest2 = new Guest("email2@gmail.com");
        
        // Act
        guest1.Participate(_event);
        var result = guest2.Participate(_event);
        
        // Assert
        Assert.False(_event.Guests.Contains(guest2));

    }
    
}