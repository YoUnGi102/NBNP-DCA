namespace UnitTests.Features.Location.UpdateName;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit.Abstractions;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit;

public class UpdateLocationNameAggregateTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Event _event;
    private Location _location;

    public UpdateLocationNameAggregateTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Location location = new Location("location", 32, [DateTime.Now.AddDays(1)]);
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public,
            EventStatus.Active, new List<Guest>(), location);
    }

    [Fact]
    public void GivenGoodName_WhenUpdatingName_ThenNameIsUpdated()
    {
        // Arrange
        var name = "new name";

        // Act
        var result = _location.UpdateName(name);

        if (result is ResultFailure<Location>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(name, result.GetObj()?.GetName());
    }

    [Fact]
    public void GivenTooLongName_WhenUpdatingName_ThenNameIsNotUpdated()
    {
        // Arrange
        string name = new string('a', 31);

        // Act
        var result = _location.UpdateName(name);
        if (result is ResultFailure<Location>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(name, result.GetObj()?.GetName());
    }

    [Fact]
    public void GivenTooShortName_WhenUpdatingName_ThenNameIsNotUpdated()
    {
        // Arrange
        string name = new string('a', 2);

        // Act
        var result = _location.UpdateName(name);
        if (result is ResultFailure<Location>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(name, result.GetObj()?.GetName());
    }
}