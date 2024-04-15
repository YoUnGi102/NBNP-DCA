using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features.Guest;

public class RemoveParticipationHandler : ICommandHandler<RemoveParticipationCommand>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _uow;

    public RemoveParticipationHandler(IGuestRepository guestRepository, IEventRepository eventRepository, IUnitOfWork uow)
    {
        _guestRepository = guestRepository;
        _eventRepository = eventRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(RemoveParticipationCommand command)
    {
        if (command is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        }

        var guest = await _guestRepository.GetByEmailAsync(command.Email);
        if (guest is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Guest not found."]);
        }

        var _event = await _eventRepository.GetAsync(command.EventId);
        if (_event is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Event not found."]);
        }

        var result = guest.RemoveParticipation(_event);
        if (result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages());
        }
        
        await _uow.SaveChangesAsync();

        return ResultSuccess<None>.CreateMessageResult(new None(), ["Participation removed."]);
    }
}