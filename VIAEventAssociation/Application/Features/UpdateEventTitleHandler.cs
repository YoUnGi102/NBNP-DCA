using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateEventTitleHandler : ICommandHandler<UpdateEventTitleCommand>
{
    private readonly IEventRepository repository;
    private readonly IUnitOfWork uow;
    
    public UpdateEventTitleHandler(IEventRepository repository, IUnitOfWork uow)
     => (this.repository, this.uow) = (repository, uow);
    
    public async Task<Result<None>> HandleAsync(UpdateEventTitleCommand command)
    {
        Event? evt = await repository.GetAsync(command.Id);
        Result<Event> result = evt.UpdateTitle(command.Title);
        
        if(result.IsFailure())
        {
            // TODO Fix this
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages() ?? []);
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<None>.CreateEmptyResult();
    }
}