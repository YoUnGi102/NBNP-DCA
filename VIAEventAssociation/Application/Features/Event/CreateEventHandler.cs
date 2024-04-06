using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class CreateEventHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IEventRepository eventRepository;
    private readonly ILocationRepository locationRepository;
    private readonly IUnitOfWork uow;
    
    public CreateEventHandler(IEventRepository eventRepository, ILocationRepository locationRepository, IUnitOfWork uow)
     => (this.eventRepository, this.locationRepository, this.uow) = (eventRepository, locationRepository, uow);
    
    public async Task<Result<None>> HandleAsync(CreateEventCommand? command)
    {
        if(command is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        
        Location location = await locationRepository.GetAsync(command.LocationId);
        Event newEvent = new Event(command.Title, command.Description, command.StartDateTime, command.EndDateTime, command.MaxGuests, command.Visibility, EventStatus.Active, new List<Guest>(), location);

        Event? result = await eventRepository.SaveAsync(newEvent);
        
        if(result == null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["The event could not be created"]);
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<None>.CreateEmptyResult();
    }
}