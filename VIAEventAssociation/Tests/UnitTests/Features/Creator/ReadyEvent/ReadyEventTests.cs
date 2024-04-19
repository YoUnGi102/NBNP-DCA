namespace UnitTests.Features.Creator.ReadyEvent;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using Domain.Aggregates.Creator;
using Domain.Aggregates.Events;
using Domain.Common.Enums;
using Domain.Aggregates.Guests;

public class ReadyEventTests
{
    private ITestOutputHelper _testOutputHelper;
    private Location _location;

    public ReadyEventTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32);
    }

    [Fact]
    public void ReadyEvent_WhenEventIsCancelled_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Cancelled, new List<Guest>(), _location);

        // Act
        var result = creator.ReadyEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(EventStatus.Ready, result.GetObj().Status);
    }

    [Fact]
    public void ReadyEvent_WhenEventIsDeleted_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Deleted, new List<Guest>(), _location);

        // Act
        var result = creator.ReadyEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(EventStatus.Ready, result.GetObj().Status);
    }

    [Fact]
    public void ReadyEvent_WhenEventIsActive_ShouldNotReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Active, new List<Guest>(), _location);

        // Act
        var result = creator.ReadyEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(EventStatus.Ready, result.GetObj().Status);
    }

    [Fact]
    public void ReadyEvent_WhenEventIsReady_ShouldReadyEvent()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var _event = new Event(1, "event", "description", DateTime.Now, DateTime.Now, 10, EventVisibility.Public,
            EventStatus.Ready, new List<Guest>(), _location);

        // Act
        var result = creator.ReadyEvent(_event);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(EventStatus.Ready, result.GetObj().Status);
    }
}