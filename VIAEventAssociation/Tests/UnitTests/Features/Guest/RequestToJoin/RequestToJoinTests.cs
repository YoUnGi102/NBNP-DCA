using Domain.Common.Entities;
using Xunit;

namespace UnitTests.Features.Guest.RequestToJoin;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class RequestToJoinTests
{
    [Fact]
    public void Request_to_join_WhenInvitationIsNotCreated_ShouldCreateInvitation()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Active, new List<Guest>());
        var guest = new Guest("email@gmail.com", null, null);
        var request = new Request(RequestStatus.Unanswered);
        
        // Act
        request.SetEvent(_event);
        // guest.RequestToJoin(_event);
        var result = guest.RequestToJoin(_event);
        
        // Assert
        Assert.True(result.GetObj().GetGuests().Find(a => a == guest).GetRequests().Find(request => request.GetEvent() == _event).status == RequestStatus.Unanswered);
    }
    [Fact]
    public void Request_to_join_WhenInvitationIsCreated_ShouldNotCreateInvitation()
    {
        // Arrange
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Active, new List<Guest>());
        var guest = new Guest("email@gmail.com", null, null);
        var request = new Request(RequestStatus.Unanswered);
        
        // Act
        request.SetEvent(_event);
        guest.RequestToJoin(_event);
        var result = guest.RequestToJoin(_event);
        
        // Assert
        Assert.True(result.GetObj().GetGuests().Find(a => a == guest).GetRequests().Find(request => request.GetEvent() == _event).status == RequestStatus.Unanswered);
    }
}