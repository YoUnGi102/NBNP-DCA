using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features.Guest;

public class RegisterAccountHandler : ICommandHandler<RegisterAccountCommand>
{
    private readonly IGuestRepository _repository;
    private readonly IUnitOfWork _uow;

    public RegisterAccountHandler(IGuestRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(RegisterAccountCommand command)
    {
        if (command is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        }
        
        var guest = new Domain.Aggregates.Guests.Guest(command.Email);

        await _repository.SaveAsync(guest);
        await _uow.SaveChangesAsync();

        return ResultSuccess<None>.CreateMessageResult(new None(), [ "Guest account created." ]);
    }
}