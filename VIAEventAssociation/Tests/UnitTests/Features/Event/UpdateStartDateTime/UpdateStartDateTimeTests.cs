using UnitTests.Fakes;

namespace UnitTests.Features.Event.UpdateStartDateTime;

using Domain.Aggregates.Locations;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

public class UpdateStartDateTimeTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Event _event;

    public UpdateStartDateTimeTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Location location = new Location("location", 32);
        _event = Constants.TEST_EVENT;
    }

    [Fact]
    public void GivenGoodStartDateTime_WhenUpdatingStartDateTime_ThenStartDateTimeIsUpdated()
    {
        // Arrange
        var startDateTime = DateTime.Now.AddDays(1);

        // Act
        var result = _event.UpdateStartDateTime(startDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.True(startDateTime - DateTime.Now > TimeSpan.Zero);
        Assert.Equal(startDateTime, result.GetObj()?.StartDateTime);
    }

    [Fact]
    public void GivenAPastDate_WhenUpdatingStartDateTime_ThenStartDateTimeIsNotUpdated()
    {
        // Arrange
        var startDateTime = new DateTime(2021, 1, 1);

        // Act
        var result = _event.UpdateStartDateTime(startDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(startDateTime, result.GetObj()?.StartDateTime);
    }

    [Fact]
    public void GivenATooFarInTheFutureDate_WhenUpdatingStartDateTime_ThenStartDateTimeIsNotUpdated()
    {
        // Arrange
        var startDateTime = DateTime.Now.AddYears(40);

        // Act
        var result = _event.UpdateStartDateTime(startDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(startDateTime, result.GetObj()?.StartDateTime);
    }
}