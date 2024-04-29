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

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.DeclineInvitation;

public class DeclineInvitationHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<SendInvitationCommand> _addHandler;
    private readonly ICommandHandler<DeclineInvitationCommand> _handler;

    private IGuestRepository _guestRepository;
    private IEventRepository _eventRepository;
    private IUnitOfWork _uow;

    public DeclineInvitationHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _guestRepository = new GuestRepoFake();
        _eventRepository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new DeclineInvitationHandler(_guestRepository, _eventRepository, _uow);
        _addHandler = new SendInvitationHandler(_guestRepository, _eventRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenDecliningInvitation_ThenInvitationDeclined()
    {
        // Arrange
        var res1 = await _addHandler.HandleAsync(SendInvitationCommand.Create("e2399bcd-b83b-400f-bfba-2e58cb2b2330", "3b1d8789-e982-41b4-9f77-a7459fd6f51e").GetObj());
        Result<DeclineInvitationCommand> cmd = DeclineInvitationCommand.Create("guest1@gmail.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");

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
    public async Task GivenNoInvitation_WhenDecliningInvitation_ThenInvitationNotDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> cmd = DeclineInvitationCommand.Create("guest1@gmail.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");

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