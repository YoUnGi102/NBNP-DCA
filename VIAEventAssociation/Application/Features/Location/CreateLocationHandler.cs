using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features.Location;

public class CreateLocationHandler : ICommandHandler<CreateLocationCommand>
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUnitOfWork _uow;

    public CreateLocationHandler(ILocationRepository locationRepository, IUnitOfWork uow)
    {
        _locationRepository = locationRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(CreateLocationCommand command)
    {
        if (command is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        }

        var location = new Domain.Aggregates.Locations.Location(command.Name, command.MaxCapacity);

        await _locationRepository.SaveAsync(location);
        await _uow.SaveChangesAsync();

        return ResultSuccess<None>.CreateMessageResult(new None(), ["Location created successfully."]);
    }
}