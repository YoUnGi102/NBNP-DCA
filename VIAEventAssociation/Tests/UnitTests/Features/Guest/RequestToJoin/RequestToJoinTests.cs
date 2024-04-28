using UnitTests.Fakes;

namespace UnitTests.Features.Guest.RequestToJoin;

using Domain.Aggregates.Locations;
using Domain.Common.Entities;
using Xunit;
using Xunit.Abstractions;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class RequestToJoinTests
{
    private ITestOutputHelper _testOutputHelper;
    private Location _location;

    public RequestToJoinTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32);
    }

    [Fact]
    public void Request_to_join_WhenInvitationIsNotCreated_ShouldCreateInvitation()
    {
        // Arrange
        var _event = Constants.TEST_EVENT;
        var guest = Constants.TEST_GUEST;

        var expect = guest.Requests.Count;
        
        // guest.RequestToJoin(_event);
        var result = guest.RequestToJoin(_event);

        // Assert
        Assert.True(result != null);
        Assert.Equal(expect+1, guest.Requests.Count);
    }

    [Fact]
    public void Request_to_join_WhenInvitationIsCreated_ShouldNotCreateInvitation()
    {
        // Arrange
        var _event = Constants.TEST_EVENT;
        var guest = Constants.TEST_GUEST;
        guest.RequestToJoin(_event);

        var expect = guest.Requests.Count;
        
        // guest.RequestToJoin(_event);
        var result = guest.RequestToJoin(_event);

        // Assert
        Assert.True(result.GetObj() == null);
        Assert.Equal(expect, guest.Requests.Count);
    }
}