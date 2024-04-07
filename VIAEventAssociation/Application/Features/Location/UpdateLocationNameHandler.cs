using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateLocationNameHandler : ICommandHandler<UpdateLocationNameCommand>
{
    private readonly ILocationRepository repository;
    private readonly IUnitOfWork uow;
    
    public UpdateLocationNameHandler(ILocationRepository repository, IUnitOfWork uow)
     => (this.repository, this.uow) = (repository, uow);
    
    public async Task<Result<None>> HandleAsync(UpdateLocationNameCommand? command)
    {
        if(command is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        
        Domain.Aggregates.Locations.Location? location = await repository.GetAsync(command.Id);
        Result<Domain.Aggregates.Locations.Location> result = location.UpdateName(command.Name);
        
        if(result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages() ?? []);
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<None>.CreateEmptyResult();
    }
}