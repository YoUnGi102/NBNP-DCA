using Domain.Aggregates.Locations;

namespace UnitTests.Features.Event.SetMaxGuests;

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
    private Location _location;

    public SetMaxGuestsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now.AddHours(1), 30,
            EventVisibility.Public, EventStatus.Active, new List<Guest>(), _location);
    }
    
    [Fact]
    public void GivenGoodMaxGuests_WhenSettingMaxGuests_ThenMaxGuestsIsSet()
    {
        // Arrange
        var maxGuests = 20;

        // Act
        var result = _event.SetMaxGuests(maxGuests);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(maxGuests, result.GetObj()?.GetMaxGuests());
    }
    
    [Fact]
    public void GivenMaxGuestsLessThanZero_WhenSettingMaxGuests_ThenMaxGuestsIsNotSet()
    {
        // Arrange
        var maxGuests = -1;

        // Act
        var result = _event.SetMaxGuests(maxGuests);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(maxGuests, result.GetObj()?.GetMaxGuests());
    }
    
    [Fact]
    public void GivenMaxGuestsEqualsZero_WhenSettingMaxGuests_ThenMaxGuestsIsNotSet()
    {
        // Arrange
        var maxGuests = 0;

        // Act
        var result = _event.SetMaxGuests(maxGuests);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(maxGuests, result.GetObj()?.GetMaxGuests());
    }
    
    [Fact]
    public void GivenMaxGuestsMoreThanLocationCapacity_WhenSettingMaxGuests_ThenMaxGuestsIsNotSet()
    {
        // Arrange
        var maxGuests = 40;

        // Act
        var result = _event.SetMaxGuests(maxGuests);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.NotEqual(maxGuests, result.GetObj()?.GetMaxGuests());
    }
}