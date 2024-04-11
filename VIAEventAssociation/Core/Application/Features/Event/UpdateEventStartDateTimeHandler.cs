using System;
using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features
{
    public class UpdateEventStartDateTimeHandler : ICommandHandler<UpdateEventStartDateTimeCommand>
    {
        private readonly IEventRepository _repository;
        private readonly IUnitOfWork _uow;

        public UpdateEventStartDateTimeHandler(IEventRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<Result<None>> HandleAsync(UpdateEventStartDateTimeCommand? command)
        {
            if (command is null)
            {
                return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
            }

            Event? evt = await _repository.GetAsync(command.Id);
            Result<Event> result = evt.UpdateStartDateTime(command.StartDateTime);

            if (result.IsFailure())
            {
                return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages() ?? []);
            }

            await _uow.SaveChangesAsync();
            return ResultSuccess<None>.CreateEmptyResult();
        }
    }
}