﻿namespace UnitTests.Features.Creator.ReadyEvent;

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
        Location location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
    }

    [Fact]
    public void SendInvitation_WhenGoodInput_ShouldSendInvitation()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var guest = new Guest("guest@mail.com", [], []);
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Cancelled, [], _location);

        var expect = guest.GetInvitations().Count + 1;
        
        // Act
        var result = creator.SendInvitation(guest, _event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(expect, guest.GetInvitations().Count);
    }

    [Fact]
    public void SendInvitation_WhenInvitationExists_ShouldNotSendInvitation()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var guest = new Guest("guest@mail.com", [], []);
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Cancelled, [], _location);

        creator.SendInvitation(guest, _event);
        
        var expect = guest.GetInvitations().Count;
        
        // Act
        var result = creator.SendInvitation(guest, _event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(expect, guest.GetInvitations().Count);
    }
}