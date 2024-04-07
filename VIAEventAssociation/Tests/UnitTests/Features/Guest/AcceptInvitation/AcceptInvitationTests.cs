namespace UnitTests.Features.Guest.AcceptInvitation;

using Domain.Aggregates.Locations;
using Domain.Common.Entities;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
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
        var _event = new Event(1, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = new Guest(1, "email@gmail.com");
        guest.SendInvitation(invitation);
        
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
        var _event = new Event(1, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Declined, _event);
        var guest = new Guest(1,"email@gmail.com");
        guest.SendInvitation(invitation);
        
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
        var _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 1,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest1 = new Guest("email@gmail.com");
        var guest2 = new Guest("email@gmail.org");
        
        guest2.SendInvitation(invitation);
        
        _event.AddGuest(guest1);

        // Act
        var result = guest2.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.False(result.GetObj().GetStatus().Equals(InvitationStatus.Accepted));
        Assert.DoesNotContain(guest2, result.GetObj().GetEvent().GetGuests());
    }
}