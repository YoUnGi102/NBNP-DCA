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

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.RemoveParticipation;

public class RemoveParticipationHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<AddParticipationCommand> _addHandler;
    private readonly ICommandHandler<RemoveParticipationCommand> _handler;

    private IGuestRepository _guestRepository;
    private IEventRepository _eventRepository;
    private IUnitOfWork _uow;

    public RemoveParticipationHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _guestRepository = new GuestRepoFake();
        _eventRepository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _addHandler = new AddParticipationHandler(_guestRepository, _eventRepository, _uow);
        _handler = new RemoveParticipationHandler(_guestRepository, _eventRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenRemovingParticipation_ThenParticipationRemoved()
    {
        // Arrange
        Result<AddParticipationCommand> cm = AddParticipationCommand.Create("guest1@gmail.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        await _addHandler.HandleAsync(cm.GetObj());
        
        Result<RemoveParticipationCommand> cmd = RemoveParticipationCommand.Create("guest1@gmail.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");

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
    public async Task GivenGuestNotInEvent_WhenRemovingParticipation_ThenParticipationNotRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> cmd = RemoveParticipationCommand.Create("guest1@gmail.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");

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