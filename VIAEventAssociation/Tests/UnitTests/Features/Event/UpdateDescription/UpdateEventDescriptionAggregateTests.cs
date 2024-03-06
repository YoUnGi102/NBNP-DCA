using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.UpdateDescription;

using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit;

public class UpdateEventDescriptionAggregateTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Event _event;

    public UpdateEventDescriptionAggregateTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public,
            EventStatus.Active, new List<Domain.Aggregates.Guests.Guest>());
    }

    [Fact]
    public void GivenGoodDescription_WhenUpdatingDescription_ThenDescriptionIsUpdated()
    {
        // Arrange
        var description = "New Description";

        // Act
        var result = _event.UpdateDescription(description);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(description, result.GetObj()?.GetDescription());
    }

    [Fact]
    public void GivenTooLongDescription_WhenUpdatingDescription_ThenDescriptionIsNotUpdated()
    {
        // Arrange
        string description = new string('a', 701);

        // Act
        var result = _event.UpdateDescription(description);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(description, result.GetObj()?.GetDescription());
    }

    [Fact]
    public void GivenTooShortDescription_WhenUpdatingDescription_ThenDescriptionIsNotUpdated()
    {
        // Arrange
        string description = new string('a', 10);

        // Act
        var result = _event.UpdateDescription(description);
        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(description, result.GetObj()?.GetDescription());
    }
}