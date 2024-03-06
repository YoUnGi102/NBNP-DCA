namespace UnitTests.Features.Event.UpdateStartDateTime;
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
    
    public UpdateStartDateTimeTests()
    {
        _event = new Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public, EventStatus.Active, new List<Domain.Aggregates.Guests.Guest>());
    }
    
    [Fact]
    public void GivenGoodStartDateTime_WhenUpdatingStartDateTime_ThenStartDateTimeIsUpdated()
    {
        // Arrange
        var currentTime = DateTime.Now;
        var startDateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day+1);
        
        // Act
        var result = _event.UpdateStartDateTime(startDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.True(startDateTime - DateTime.Now > TimeSpan.Zero);
        Assert.Equal(startDateTime, result.GetObj()?.GetStartDateTime());
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
        Assert.NotEqual(startDateTime, result.GetObj()?.GetStartDateTime());
    }
    
    [Fact]
    
    public void GivenATooFarInTheFutureDate_WhenUpdatingStartDateTime_ThenStartDateTimeIsNotUpdated()
    {
        // Arrange
        var startDateTime = new DateTime(DateTime.Now.Year+30, 1, 1);

        // Act
        var result = _event.UpdateStartDateTime(startDateTime);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.NotEqual(startDateTime, result.GetObj()?.GetStartDateTime());
    }
}