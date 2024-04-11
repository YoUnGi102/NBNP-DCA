using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Entities;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features.Guest;

public class DeclineInvitationHandler : ICommandHandler<DeclineInvitationCommand>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _uow;

    public DeclineInvitationHandler(IGuestRepository guestRepository, IEventRepository eventRepository, IUnitOfWork uow)
    {
        _guestRepository = guestRepository;
        _eventRepository = eventRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(DeclineInvitationCommand command)
    {
        if (command is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        }

        var guest = await _guestRepository.GetAsync(command.Email);
        if (guest is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Guest not found."]);
        }

        var _event = await _eventRepository.GetAsync(command.EventId);
        if (_event is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Event not found."]);
        }

        var result = guest.DeclineInvitation(new Invitation(_event, guest));
        if (result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages()!);
        }
        
        await _eventRepository.SaveAsync(_event);
        await _uow.SaveChangesAsync();

        return ResultSuccess<None>.CreateMessageResult(new None(), ["Invitation declined."]);
    }
}