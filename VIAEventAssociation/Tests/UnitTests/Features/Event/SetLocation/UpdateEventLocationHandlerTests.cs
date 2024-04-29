using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class SetEventLocationHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<SetEventLocationCommand> _handler;
    private IEventRepository _repository;
    private ILocationRepository _locationRepository;
    private IUnitOfWork _uow;

    public SetEventLocationHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _locationRepository = new LocationRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new SetEventLocationHandler(_repository, _locationRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenSettingLocation_ThenLocationSet()
    {
        // Arrange
        Result<SetEventLocationCommand> locationCommand = SetEventLocationCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", "7c59adac-5a10-4de9-8783-ea2add07bb65");

        // Act
        if (locationCommand.IsFailure())
        {
            foreach (var error in locationCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(locationCommand.GetObj());
        
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        
        // Assert
        Assert.False(result.IsFailure());
        
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingLocation_ThenLocationNotSet()
    {
        // Arrange
        Result<SetEventLocationCommand> locationCommand = SetEventLocationCommand.Create("", "");

        // Act
        if (locationCommand.IsFailure())
        {
            foreach (var error in locationCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(locationCommand.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
    
}