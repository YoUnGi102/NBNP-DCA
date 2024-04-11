using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class SetEventLocationHandler : ICommandHandler<SetEventLocationCommand>
{
    private readonly IEventRepository repository;
    private readonly ILocationRepository locationRepository;
    private readonly IUnitOfWork uow;
    
    public SetEventLocationHandler(IEventRepository repository, ILocationRepository locationRepository, IUnitOfWork uow)
     => (this.repository, this.locationRepository, this.uow) = (repository, locationRepository, uow);
    
    public async Task<Result<None>> HandleAsync(SetEventLocationCommand? command)
    {
        if(command is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);

        Domain.Aggregates.Locations.Location loc = await locationRepository.GetAsync(command.LocationId);
        if (loc is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Location not found."]);
        
        Event? evt = await repository.GetAsync(command.EventId);
        Result<Event> result = evt.SetLocation(loc);
        
        if(result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages() ?? []);
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<None>.CreateEmptyResult();
    }
}