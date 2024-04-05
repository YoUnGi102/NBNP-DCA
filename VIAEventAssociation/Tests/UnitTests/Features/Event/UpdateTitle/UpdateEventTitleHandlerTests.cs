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

public class UpdateEventTitleHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateEventTitleCommand> _handler;
    private Domain.Aggregates.Events.Event _event;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public UpdateEventTitleHandlerTests()
    {
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new UpdateEventTitleHandler(_repository, _uow);
        
        Location location = new Location("location", 32, new List<DateTime> { DateTime.Now.AddDays(1) });
        _event = new Domain.Aggregates.Events.Event(0, "Title", "Description", DateTime.Now, DateTime.Now, 30, EventVisibility.Public,
            EventStatus.Active, new List<Guest>(), location);
    }

    [Fact]
    public async Task Handle_ValidInput_Success()
    {
        // Arrange
        Result<UpdateEventTitleCommand> titleCommand = UpdateEventTitleCommand.Create(1, "New Title");

        // Act
        if (titleCommand.IsFailure())
        {
            foreach (var error in titleCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(titleCommand.GetObj());
        
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        
        // Assert
        Assert.False(result.IsFailure());
        
    }
    
    [Fact]
    public async Task Handle_InvalidInput_Failure()
    {
        // Arrange
        Result<UpdateEventTitleCommand> titleCommand = UpdateEventTitleCommand.Create(1, "");

        // Act
        if (titleCommand.IsFailure())
        {
            foreach (var error in titleCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(titleCommand.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
    
}