using Domain.Aggregates.Locations;
using UnitTests.Fakes;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit;

public class UpdateEventTitleAgregateTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Event _event;

    public UpdateEventTitleAgregateTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Domain.Aggregates.Locations.Location location = new Domain.Aggregates.Locations.Location("location", 32);
        _event = Constants.TEST_EVENT;
    }

    [Fact]
    public void GivenGoodTitle_WhenUpdatingTitle_ThenTitleIsUpdated()
    {
        // Arrange
        var title = "New Title";

        // Act
        var result = _event.UpdateTitle(title);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(title, result.GetObj().Title);
    }

    [Fact]
    public void GivenNoTitle_WhenUpdatingTitle_ThenTitleIsNotUpdated()
    {
        // Arrange
        var title = "";

        // Act
        var result = _event.UpdateTitle(title);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(title, (result.GetObj().Title));
    }

    [Fact]
    public void GivenTooLongTitle_WhenUpdatingTitle_ThenTitleIsNotUpdated()
    {
        // Arrange
        string title = new string('a', 101);

        // Act
        var result = _event.UpdateTitle(title);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(title, result.GetObj().Title);
    }

    [Fact]
    public void GivenTooShortTitle_WhenUpdatingTitle_ThenTitleIsNotUpdated()
    {
        // Arrange
        string title = new string('a', 1);

        // Act
        var result = _event.UpdateTitle(title);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(title, result.GetObj().Title);
    }
}