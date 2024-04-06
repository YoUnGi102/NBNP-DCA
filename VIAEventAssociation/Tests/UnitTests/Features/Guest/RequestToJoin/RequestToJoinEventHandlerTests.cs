using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.Features.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.RequestToJoin;

public class RequestToJoinEventHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<RequestJoinEventCommand> _handler;
    private IEventRepository _eventRepository;
    private IGuestRepository _guestRepository;
    private IUnitOfWork _uow;

    public RequestToJoinEventHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _eventRepository = new EventRepoFake();
        _guestRepository = new GuestRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new RequestJoinEventHandler(_eventRepository, _guestRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenRequestingToJoin_ThenRequestCreated()
    {
        // Arrange
        Result<RequestJoinEventCommand> joinCommand = RequestJoinEventCommand.Create(1, 1);

        // Act
        if (joinCommand.IsFailure())
        {
            foreach (var error in joinCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(joinCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }

    [Fact]
    public async Task GivenRequestExists_WhenRequestingToJoin_ThenRequestNotCreated()
    {
        // Arrange
        Result<RequestJoinEventCommand> joinCommand = RequestJoinEventCommand.Create(1, 1);
        await _handler.HandleAsync(joinCommand.GetObj());
        
        // Act
        if (joinCommand.IsFailure())
        {
            foreach (var error in joinCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(joinCommand.GetObj());

        // Assert
        Assert.True(result.IsFailure());
    }
}