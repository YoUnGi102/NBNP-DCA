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
        _location = new Location("location", 32);
    }
    
    [Fact]
    public void DeclineInvitation_WhenInvitationIsUnanswered_ShouldDeclineInvitation()
    {
        // Arrange
        var _event = new Domain.Aggregates.Events.Event(new Guid(), "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = new Guest(new Guid(), "email@gmail.com");
        guest.SendInvitation(invitation);
        
        // Act
        var result = guest.DeclineInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.True(result.GetObj().GetStatus().Equals(InvitationStatus.Declined));
    }
}