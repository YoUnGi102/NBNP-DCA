using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using UnitTests.Features.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class CreateEventHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<CreateEventCommand> _handler;
    private Domain.Aggregates.Events.Event _event;

    private ILocationRepository _locationRepository;
    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public CreateEventHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _locationRepository = new LocationRepoFake();
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new CreateEventHandler(_repository, _locationRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenCreatingEvent_ThenEventCreated()
    {
        // Arrange
        Result<CreateEventCommand> cmd = CreateEventCommand.Create(
            "Chess Tournament", 
            "Monthly Chess Tournament by VIA Chess Club", 
            Constants.START_DATE_STRING,
            Constants.END_DATE_STRING, 
            100, 
            "Public",
            "Active", 
            1);
        // Act
        
        var result = await _handler.HandleAsync(cmd.GetObj());
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenCreatingEvent_ThenEventNotCreated()
    {
        // Arrange
        Result<CreateEventCommand> cmd = CreateEventCommand.Create(
            "Chess Tournament",
            "Monthly Chess Tournament by VIA Chess Club",
            Constants.END_DATE_STRING,
            Constants.START_DATE_STRING,
            -1,
            "Public",
            "Active",
            1);

        // Act
        if (cmd.IsFailure())
        {
            foreach (var error in cmd.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(cmd.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
    
}