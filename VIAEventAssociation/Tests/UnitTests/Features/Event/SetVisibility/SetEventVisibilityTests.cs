using UnitTests.Fakes;

namespace UnitTests.Features.Event.SetVisibility;

using Domain.Aggregates.Locations;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Enums;
using Xunit.Abstractions;

using Xunit;
public class SetEventVisibilityTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Event _event;
    
    public SetEventVisibilityTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Location location = new Location("location", 32);
        _event = Constants.TEST_EVENT;
    }
    
    [Fact]
    public void GivenPrivateVisibility_WhenSettingVisibility_ThenVisibilityIsPrivate()
    {
        // Arrange
        var visibility = EventVisibility.Private;
        
        // Act
        var result = _event.SetVisibility(visibility);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.Equal(visibility, result.GetObj()?.Visibility);
    }
    
    [Fact]
    public void GivenPublicVisibility_WhenSettingVisibility_ThenVisibilityIsPublic()
    {
        // Arrange
        var visibility = EventVisibility.Public;
        
        // Act
        var result = _event.SetVisibility(visibility);

        if (result is ResultFailure<Event>)
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        
        // Assert
        Assert.Equal(visibility, result.GetObj()?.Visibility);
    }
}