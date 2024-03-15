namespace UnitTests.Features.Guest.DeclineInvitation;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;
using Domain.Common.Entities;

public class DeclineInvitationTests
{
    private ITestOutputHelper _testOutputHelper;
    private Location _location;
    
    public DeclineInvitationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
    }
    
    [Fact]
    public void DeclineInvitation_WhenInvitationIsUnanswered_ShouldDeclineInvitation()
    {
        // Arrange
        var _event = new Domain.Aggregates.Events.Event(0, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = new Guest("email@gmail.com");
        
        // Act
        var result = guest.DeclineInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // // Arrange
        // var invitation = new Invitation(InvitationStatus.Unanswered, null);
        // var invitations = new List<Invitation>();
        // invitations.Add(invitation);
        // var guest = new Guest("email@gmail.com");
        //
        // // Act
        // invitations.Add(invitation);
        // var result = guest.DeclineInvitation(invitation);
        
        // Assert
        Assert.True(result.GetObj().GetStatus().Equals(InvitationStatus.Declined));
    }
}