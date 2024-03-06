using Xunit;

namespace UnitTests.Features.Guest.RemoveParticipation;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class RemoveParticipationTests
{
    [Fact]
    public void RemoveParticipation_WhenEventIsReady_ShouldRemoveParticipation()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public, EventStatus.Cancelled, new List<Guest>());
        var guest = new Guest("email@gmail.com", null, null);
        // Act
        guest.Participate(_event);
        guest.RemoveParticipation(_event);

        // Assert
        Assert.False(_event.GetGuests().Contains(guest));
    }

    [Fact]
    public void RemoveParticipation_whenGuestIsNotParticipating_ShouldNotRemoveParticipation()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Cancelled, new List<Guest>());
        var guest = new Guest("email@gmail.com", null, null);
        
        // Act
        
        
        // Assert
        Assert.Throws(typeof(InvalidOperationException), () => guest.RemoveParticipation(_event));
    }
}