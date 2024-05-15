using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.Features.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.AcceptInvitation;

public class AcceptInvitationHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<SendInvitationCommand> _addHandler;
    private readonly ICommandHandler<AcceptInvitationCommand> _handler;

    private IGuestRepository _guestRepository;
    private IEventRepository _eventRepository;
    private IUnitOfWork _uow;

    public AcceptInvitationHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _guestRepository = new GuestRepoFake();
        _eventRepository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new AcceptInvitationHandler(_guestRepository, _eventRepository, _uow);
        _addHandler = new SendInvitationHandler(_guestRepository, _eventRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenAcceptingInvitation_ThenInvitationAccepted()
    {
        // Arrange
        var res1 = await _addHandler.HandleAsync(SendInvitationCommand.Create(Guid.NewGuid(), Guid.NewGuid()).GetObj());
        Result<AcceptInvitationCommand> cmd = AcceptInvitationCommand.Create("guest1@gmail.com", Guid.NewGuid());

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
    public async Task GivenNoInvitation_WhenAcceptingInvitation_ThenInvitationNotAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> cmd = AcceptInvitationCommand.Create("guest1@gmail.com", Guid.NewGuid());

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