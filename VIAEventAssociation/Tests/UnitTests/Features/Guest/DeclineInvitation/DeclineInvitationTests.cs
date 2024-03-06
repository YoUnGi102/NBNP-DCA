using Xunit;

namespace UnitTests.Features.Guest.DeclineInvitation;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;
using Domain.Common.Entities;

public class DeclineInvitationTests
{
    [Fact]
    public void DeclineInvitation_WhenInvitationIsUnanswered_ShouldAcceptInvitation()
    {
        // Arrange
        var invitation = new Invitation(InvitationStatus.Unanswered, null);
        var invitations = new List<Invitation>();
        invitations.Add(invitation);
        var guest = new Guest("email@gmail.com", null, null);
        
        // Act
        invitations.Add(invitation);
        var result = guest.DeclineInvitation(invitation);
        
        // Assert
        Assert.True(result.GetObj().GetStatus().Equals(InvitationStatus.Declined));
    }
}