namespace UnitTests.Features.Location.UpdateName;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit.Abstractions;
using Xunit;

public class SetLocationMaxCapacityAggregateTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Location _location;

    public SetLocationMaxCapacityAggregateTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32, [DateTime.Now.AddDays(1)]);
    }

    [Fact]
    public void GivenGoodCapacity_WhenSettingCapacity_ThenCapacityIsUpdated()
    {
        // Arrange
        var capacity = 40;

        // Act
        var result = _location.SetMaxCapacity(capacity);

        if (result is ResultFailure<Location>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(capacity, result.GetObj()?.GetMaxCapacity());
    }

    [Fact]
    public void GivenNonPositiveNumber_WhenSettingCapacity_ThenCapacityIsNotUpdated()
    {
        // Arrange
        var capacity = 0;

        // Act
        var result = _location.SetMaxCapacity(capacity);

        if (result is ResultFailure<Location>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());

        // Assert
        Assert.Equal(capacity, result.GetObj()?.GetMaxCapacity());
    }
}