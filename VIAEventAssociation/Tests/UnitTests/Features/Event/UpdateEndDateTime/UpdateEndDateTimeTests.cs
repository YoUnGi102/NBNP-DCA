using UnitTests.Fakes;

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
        _event = Constants.TEST_EVENT;
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
        Assert.Equal(endDateTime, result.GetObj()?.EndDateTime);
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
        Assert.NotEqual(endDateTime, result.GetObj()?.EndDateTime);
    }

    [Fact]
    public void GivenEndDateTimeEqualsStartDateTime_WhenUpdatingEndDateTime_ThenEndDateTimeIsNotUpdated()
    {
        // Arrange
        var endDateTime = _event.StartDateTime;

        // Act
        var result = _event.UpdateEndDateTime(endDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(endDateTime, result.GetObj()?.EndDateTime);
    }
}