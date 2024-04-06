using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class SetEventVisibilityHandler : ICommandHandler<SetEventVisibilityCommand>
{
    private readonly IEventRepository repository;
    private readonly IUnitOfWork uow;
    
    public SetEventVisibilityHandler(IEventRepository repository, IUnitOfWork uow)
     => (this.repository, this.uow) = (repository, uow);
    
    public async Task<Result<None>> HandleAsync(SetEventVisibilityCommand? command)
    {
        if(command is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        
        Event? evt = await repository.GetAsync(command.Id);
        Result<Event> result = evt.SetVisibility(command.Visibility);
        
        if(result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages() ?? []);
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<None>.CreateEmptyResult();
    }
}