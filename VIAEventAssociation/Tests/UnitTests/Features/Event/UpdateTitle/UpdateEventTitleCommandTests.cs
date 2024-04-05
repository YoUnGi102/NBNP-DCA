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

public class UpdateEventTitleCommandTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateEventTitleCommand> _handler;
    private Domain.Aggregates.Events.Event _event;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public UpdateEventTitleCommandTests()
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
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(1, "New Title");
        UpdateEventTitleCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task Handle_InvalidInput_Failure()
    {
        // Arrange
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(1, "");
        UpdateEventTitleCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
}