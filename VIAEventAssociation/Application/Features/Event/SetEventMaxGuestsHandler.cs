using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class SetEventMaxGuestsHandler : ICommandHandler<SetEventMaxGuestsCommand>
{
    private readonly IEventRepository repository;
    private readonly IUnitOfWork uow;
    
    public SetEventMaxGuestsHandler(IEventRepository repository, IUnitOfWork uow)
     => (this.repository, this.uow) = (repository, uow);
    
    public async Task<Result<None>> HandleAsync(SetEventMaxGuestsCommand? command)
    {
        if(command is null)
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        
        Event? evt = await repository.GetAsync(command.Id);
        Result<Event> result = evt.SetMaxGuests(command.MaxGuests);
        
        if(result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages() ?? []);
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<None>.CreateEmptyResult();
    }
}