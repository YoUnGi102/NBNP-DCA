using UnitTests.Fakes;

namespace UnitTests.Features.Creator.ReadyEvent;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class SendInvitationAggregateTest
{
    private ITestOutputHelper _testOutputHelper;
    private Location _location;

    public SendInvitationAggregateTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32);
    }

    [Fact]
    public void SendInvitation_WhenGoodInput_ShouldSendInvitation()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var guest = Constants.TEST_GUEST;
        var _event = Constants.TEST_EVENT;

        var expect = guest.Invitations.Count + 1;
        
        // Act
        var result = creator.SendInvitation(guest, _event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(expect, guest.Invitations.Count);
    }

    [Fact]
    public void SendInvitation_WhenInvitationExists_ShouldNotSendInvitation()
    {
        // Arrange
        var creator = Constants.TEST_CREATOR;
        var guest = Constants.TEST_GUEST;
        var _event = Constants.TEST_EVENT;

        creator.SendInvitation(guest, _event);
        
        var expect = guest.Invitations.Count;
        
        // Act
        var result = creator.SendInvitation(guest, _event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(expect, guest.Invitations.Count);
    }
}