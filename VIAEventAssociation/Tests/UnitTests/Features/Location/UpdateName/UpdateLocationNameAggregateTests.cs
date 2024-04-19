using Domain.Common.Entities;

namespace UnitTests.Features.Location.UpdateName;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit.Abstractions;
using Xunit;

public class UpdateLocationNameAggregateTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Location _location;

    public UpdateLocationNameAggregateTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _location = new Location("location", 32);
        _location.SetAvailability(DateTime.Now, DateTime.Now.AddDays(1));
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
        Assert.Equal(name, result.GetObj()?.Name);
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
        Assert.NotEqual(name, result.GetObj()?.Name);
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
        Assert.NotEqual(name, result.GetObj()?.Name);
    }
}