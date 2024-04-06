using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.Entities;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features.Guest;

public class RequestJoinEventHandler : ICommandHandler<RequestJoinEventCommand>
{
    private readonly IEventRepository eventRepository;
    private readonly IGuestRepository guestRepository;
    private readonly IUnitOfWork uow;

    public RequestJoinEventHandler(IEventRepository eventRepository, IGuestRepository guestRepository, IUnitOfWork uow)
     => (this.eventRepository, this.guestRepository, this.uow) = (eventRepository, guestRepository, uow);

    public async Task<Result<None>> HandleAsync(RequestJoinEventCommand? command)
    {
        if(command is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);

        Event? evt = await eventRepository.GetAsync(command.EventId);
        if (evt is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Event not found."]);

        Domain.Aggregates.Guests.Guest? guest = await guestRepository.GetAsync(command.GuestId);
        if (guest is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Guest not found."]);

        Result<Request> result = guest.RequestToJoin(evt);

        if(result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages() ?? []);
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<None>.CreateEmptyResult();
    }
}