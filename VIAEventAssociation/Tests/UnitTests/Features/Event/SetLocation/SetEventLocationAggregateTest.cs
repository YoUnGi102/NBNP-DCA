using UnitTests.Fakes;

namespace UnitTests.Features.Event.SetLocation;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit.Abstractions;
using Xunit;

public class SetMaxGuestsTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Event _event;

    public SetMaxGuestsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Location location = new Location("location", 32);
        _event = Constants.TEST_EVENT;
    }
    
    [Fact]
    public void GivenFreeLocation_WhenSettingLocation_ThenLocationIsSet()
    {
        // Arrange
        var newLocation = new Location("VIA University College", 40);

        // Act
        var result = _event.SetLocation(newLocation);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(newLocation.Name, result.GetObj()?.Location.Name);
    }
    
    [Fact]
    public void GivenLocationWithLowCapacity_WhenSettingLocation_ThenLocationIsNotSet()
    {
        // Arrange
        var newLocation = new Location("VIA A2.05", 20);

        // Act
        var result = _event.SetLocation(newLocation);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(newLocation.Name, result.GetObj()?.Location.Name);
    }
    
}