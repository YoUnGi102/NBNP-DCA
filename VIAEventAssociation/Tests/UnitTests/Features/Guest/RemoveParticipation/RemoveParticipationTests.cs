namespace UnitTests.Features.Guest.RemoveParticipation;

using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using Domain.Aggregates.Locations;
using Domain.Common.Entities;


using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;
public class RemoveParticipationTests
{
    private ITestOutputHelper _testOutputHelper;
    private Location _location;

    public RemoveParticipationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Location location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
    }

    [Fact]
    public void RemoveParticipation_WhenEventIsReady_ShouldRemoveParticipation()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Cancelled, new List<Guest>(), _location);
        var guest = new Guest("email@gmail.com");
        // Act
        guest.Participate(_event);
        guest.RemoveParticipation(_event);
        var result = guest.RemoveParticipation(_event);

        // Assert
        Assert.False(_event.GetGuests().Contains(guest));
    }

    [Fact]
    public void RemoveParticipation_whenGuestIsNotParticipating_ShouldNotRemoveParticipation()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Cancelled, new List<Guest>(), _location);
        var guest = new Guest("email@gmail.com");

        // Act
        var result = guest.RemoveParticipation(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.False(_event.GetGuests().Contains(guest));
    }
}