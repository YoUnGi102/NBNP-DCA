using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class UpdateEventTitleHandler : ICommandHandler<UpdateEventTitleCommand, Event>
{
    private readonly IEventRepository repository;
    private readonly IUnitOfWork uow;
    
    internal UpdateEventTitleHandler(IEventRepository repository, IUnitOfWork uow)
     => (this.repository, this.uow) = (repository, uow);
    
    public async Task<Result<Event>> HandleAsync(UpdateEventTitleCommand command)
    {
        Event evt = await repository.GetAsync(command.Id);
        Result<Event> result = evt.UpdateTitle(command.Title);
        
        if(result.isFailure())
        {
            return result;
        }

        await uow.SaveChangesAsync();
        return ResultSuccess<Event>.CreateEmptyResult();
    }
}