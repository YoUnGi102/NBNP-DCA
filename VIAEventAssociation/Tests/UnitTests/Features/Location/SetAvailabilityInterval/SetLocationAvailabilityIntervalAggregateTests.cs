﻿namespace UnitTests.Features.Location.UpdateName;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit.Abstractions;
using Xunit;

public class SetLocationAvailabilityIntervalAggregateTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Location _location;

    public SetLocationAvailabilityIntervalAggregateTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32);
    }

    [Fact]
    public void GivenGoodInterval_WhenSettingInterval_ThenIntervalIsUpdated()
    {
        // Arrange
        var start = DateTime.Now;
        var end = DateTime.Now.AddDays(1);

        // Act
        var result = _location.SetAvailability(start, end);

        if (result is ResultFailure<Location>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(result.GetObj().AvailabilityStart, DateOnly.FromDateTime(start));
        Assert.Equal(result.GetObj().AvailabilityEnd, DateOnly.FromDateTime(end));
    }

    [Fact]
    public void GivenEndDateBeforeStart_WhenSettingAvailability_ThenAvailabilityIsNotUpdated()
    {
        // Arrange
        var start = DateTime.Now.AddDays(1);
        var end = DateTime.Now;

        // Act
        var result = _location.SetAvailability(start, end);

        if (result is ResultFailure<Location>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(result.GetObj().AvailabilityStart, DateOnly.FromDateTime(start));
        Assert.Equal(result.GetObj().AvailabilityEnd, DateOnly.FromDateTime(end));
    }
}