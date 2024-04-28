using UnitTests.Fakes;

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
        var _event = Constants.TEST_EVENT;
        var invitation = new Invitation(InvitationStatus.Unanswered, _event);
        var guest = Constants.TEST_GUEST;
        guest.SendInvitation(invitation);
        
        // Act
        var result = guest.DeclineInvitation(invitation);
        if (result is ResultFailure<Invitation>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.True(result.GetObj().Status.Equals(InvitationStatus.Declined));
    }
}