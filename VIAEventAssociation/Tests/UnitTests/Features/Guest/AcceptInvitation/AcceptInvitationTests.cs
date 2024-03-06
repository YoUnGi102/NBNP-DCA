using Domain.Common.Entities;
using Xunit;

namespace UnitTests.Features.Guest.AcceptInvitation;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class AcceptInvitationTests
{
    [Fact]
    public void AcceptInvitation_WhenInvitationIsUnanswered_ShouldAcceptInvitation()
    {
        // Arrange
        var invitation = new Invitation(InvitationStatus.Unanswered);
        var invitations = new List<Invitation>();
        invitations.Add(invitation);
        var guest = new Guest("email@gmail.com", null, null);
        
        // Act
        invitations.Add(invitation);
        guest.AcceptInvitation(invitation);
        
        // Assert
        Assert.True(invitation.GetStatus().Equals(InvitationStatus.Accepted));
    }

}