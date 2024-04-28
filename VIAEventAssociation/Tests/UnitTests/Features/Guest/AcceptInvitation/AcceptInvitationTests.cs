using UnitTests.Fakes;

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
        _location = new Location("location", 32);
    }
    
    [Fact]
    public void AcceptInvitation_WhenInvitationIsUnanswered_ShouldAcceptInvitation()
    {
        // Arrange
        var _event = Constants.TEST_EVENT;
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = Constants.TEST_GUEST;
        guest.SendInvitation(invitation);
        
        // Act
        var result = guest.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.True(result.GetObj().Status.Equals(InvitationStatus.Accepted));
        Assert.Contains(guest, result.GetObj().Event.Guests);
    }
    
    [Fact]
    public void AcceptInvitation_WhenInvitationIsDeclined_ShouldAcceptInvitation()
    {
        // Arrange
        var _event = Constants.TEST_EVENT;
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = Constants.TEST_GUEST;
        guest.SendInvitation(invitation);
        
        // Act
        var result = guest.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.True(result.GetObj().Status.Equals(InvitationStatus.Accepted));
        Assert.Contains(guest, result.GetObj().Event.Guests);
    }

    [Fact]
    public void AcceptInvitation_WhenEventIsEnded_ShouldNotAcceptInvitation()
    {
        // Arrange
        var _event = Constants.TEST_EVENT;
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = Constants.TEST_GUEST;

        // Act
        var result = guest.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.False(result.GetObj().Status.Equals(InvitationStatus.Accepted));
        Assert.DoesNotContain(guest, result.GetObj().Event.Guests);
    }
    
    [Fact]
    public void AcceptInvitation_WhenMaxGuestsIsReached_ShouldNotAcceptInvitation()
    {
        // Arrange
        var _event = Constants.TEST_EVENT;
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest1 = Constants.TEST_GUEST;
        var guest2 = Constants.TEST_GUEST;
        
        guest2.SendInvitation(invitation);
        
        _event.AddGuest(guest1);

        // Act
        var result = guest2.AcceptInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.False(result.GetObj().Status.Equals(InvitationStatus.Accepted));
        Assert.DoesNotContain(guest2, result.GetObj().Event.Guests);
    }
}