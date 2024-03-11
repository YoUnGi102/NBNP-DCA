namespace UnitTests.Features.Event.UpdateEndDateTime;

using Domain.Aggregates.Locations;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

public class UpdateEndDateTimeTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Event _event;

    public UpdateEndDateTimeTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Location location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), location);
    }

    [Fact]
    public void GivenGoodEndDateTime_WhenUpdatingEndDateTime_ThenEndDateTimeIsUpdated()
    {
        // Arrange
        var endDateTime = DateTime.Now.AddHours(1);

        // Act
        var result = _event.UpdateEndDateTime(endDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(endDateTime, result.GetObj()?.GetEndDateTime());
    }

    [Fact]
    public void GivenEndDateTimeBeforeStartDateTime_WhenUpdatingEndDateTime_ThenEndDateTimeIsNotUpdated()
    {
        // Arrange
        var endDateTime = DateTime.Now.AddHours(-1);

        // Act
        var result = _event.UpdateEndDateTime(endDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(endDateTime, result.GetObj()?.GetEndDateTime());
    }

    [Fact]
    public void GivenEndDateTimeEqualsStartDateTime_WhenUpdatingEndDateTime_ThenEndDateTimeIsNotUpdated()
    {
        // Arrange
        var endDateTime = _event.GetStartDateTime();

        // Act
        var result = _event.UpdateEndDateTime(endDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(endDateTime, result.GetObj()?.GetEndDateTime());
    }
}