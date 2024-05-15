using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.Features.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Creator.SendInvitation;

public class SendInvitationHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<AddParticipationCommand> _addHandler;
    private readonly ICommandHandler<SendInvitationCommand> _handler;

    private IGuestRepository _guestRepository;
    private IEventRepository _eventRepository;
    private IUnitOfWork _uow;

    public SendInvitationHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _guestRepository = new GuestRepoFake();
        _eventRepository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new SendInvitationHandler(_guestRepository, _eventRepository, _uow);
        _addHandler = new AddParticipationHandler(_guestRepository, _eventRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenSendingInvitation_ThenInvitationSent()
    {
        // Arrange
        Result<SendInvitationCommand> cmd = SendInvitationCommand.Create(new Guid(), new Guid());

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
    public async Task GivenGuestInEvent_WhenSendingInvitation_ThenInvitationNotSent()
    {
        // Arrange
        await _addHandler.HandleAsync(AddParticipationCommand.Create("guest1@gmail.com", new Guid()).GetObj());
        Result<SendInvitationCommand> cmd = SendInvitationCommand.Create(new Guid(), new Guid());

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