using UnitTests.Fakes;

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
        var _event = Constants.TEST_EVENT;
        var guest = Constants.TEST_GUEST;
        
        // Act
        var result = guest.Participate(_event);

        // Assert
        Assert.False(_event.Guests.Contains(guest));
        
    }
    [Fact]
    public void Participate_WhenEventIsFull_ShouldNotParticipate()
    {
        // Arrange
        var _event = Constants.TEST_EVENT;
        _event.SetMaxGuests(1);
        var guest1 = Constants.TEST_GUEST;
        var guest2 = Constants.TEST_GUEST;
        
        // Act
        guest1.Participate(_event);
        var result = guest2.Participate(_event);
        
        // Assert
        Assert.False(_event.Guests.Contains(guest2));

    }
    
}