using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Domain.Common.Helpers;
using System;

namespace VIAEventAssociation.Tests.UnitTests.Features.Location.Update;

public class AddAvailabilityIntervalCommandTests
{
    [Fact]
    public async Task GivenValidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalAdded()
    {
        // Arrange
        string startDate = DateParser.ToString(DateTime.Now);
        string endDate = DateParser.ToString(DateTime.Now.AddDays(1));
        Result<AddAvailabilityIntervalCommand> result = AddAvailabilityIntervalCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", startDate, endDate);
        AddAvailabilityIntervalCommand command = result.GetObj();

        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.Equal("7c59adac-5a10-4de9-8783-ea2add07bb65", command.LocationId.ToString());
        Assert.Equal(DateParser.ParseDate(startDate), command.StartDate);
        Assert.Equal(DateParser.ParseDate(endDate), command.EndDate);
    }

    [Fact]
    public async Task GivenInvalidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalNotAdded()
    {
        // Arrange
        string startDate = DateParser.ToString(DateTime.Now);
        string endDate = DateParser.ToString(DateTime.Now.AddDays(-1)); // End date is before start date
        Result<AddAvailabilityIntervalCommand> result = AddAvailabilityIntervalCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", startDate, endDate);
        AddAvailabilityIntervalCommand command = result.GetObj();

        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
}