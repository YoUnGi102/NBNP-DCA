using Domain.Aggregates.Locations;
using Domain.Common.Entities;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Guest.AcceptInvitation;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Events;

public class AcceptInvitationTests
{
    private ITestOutputHelper _testOutputHelper;
    private Location _location;
    
    public AcceptInvitationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
        
    }
    
    [Fact]
    public void AcceptInvitation_WhenInvitationIsUnanswered_ShouldAcceptInvitation()
    {
        // Arrange
        var _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = new Guest("email@gmail.com");
        
        // Act
        var result = guest.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.True(result.GetObj().GetStatus().Equals(InvitationStatus.Accepted));
        Assert.Contains(guest, result.GetObj().GetEvent().GetGuests());
    }
    
    [Fact]
    public void AcceptInvitation_WhenInvitationIsDeclined_ShouldAcceptInvitation()
    {
        // Arrange
        var _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Declined, _event);
        var guest = new Guest("email@gmail.com");
        
        // Act
        var result = guest.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.True(result.GetObj().GetStatus().Equals(InvitationStatus.Accepted));
        Assert.Contains(guest, result.GetObj().GetEvent().GetGuests());
    }

    [Fact]
    public void AcceptInvitation_WhenEventIsEnded_ShouldNotAcceptInvitation()
    {
        // Arrange
        var _event = new Event(0, "Title", "Description", DateTime.Now.AddHours(-3), DateTime.Now.AddHours(-1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = new Guest("email@gmail.com");

        // Act
        var result = guest.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.False(result.GetObj().GetStatus().Equals(InvitationStatus.Accepted));
        Assert.DoesNotContain(guest, result.GetObj().GetEvent().GetGuests());
    }
    
    [Fact]
    public void AcceptInvitation_WhenMaxGuestsIsReached_ShouldNotAcceptInvitation()
    {
        // Arrange
        var _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 0,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = new Guest("email@gmail.com");

        // Act
        // guest.AcceptInvitation(invitation);
        // guest.AcceptInvitation(invitation);
        // guest.AcceptInvitation(invitation);
        var result = guest.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.False(result.GetObj().GetStatus().Equals(InvitationStatus.Accepted));
        Assert.DoesNotContain(guest, result.GetObj().GetEvent().GetGuests());
    }
}