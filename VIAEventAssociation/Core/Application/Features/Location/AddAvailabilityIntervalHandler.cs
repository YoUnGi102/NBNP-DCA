using System;
using Domain.Aggregates.Locations;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using System.Threading.Tasks;
using Domain.Aggregates.Events;

namespace ViaEventAssociation.Core.Application.Features.Location;

public class AddAvailabilityIntervalHandler : ICommandHandler<AddAvailabilityIntervalCommand>
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUnitOfWork _uow;

    public AddAvailabilityIntervalHandler(ILocationRepository locationRepository, IUnitOfWork uow)
    {
        _locationRepository = locationRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(AddAvailabilityIntervalCommand command)
    {
        if (command is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
        }

        var location = await _locationRepository.GetAsync(command.LocationId);
        if (location is null)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["Location not found."]);
        }

        var result = location.SetAvailability(command.StartDate, command.EndDate);

        if (result.IsFailure())
        {
            return ResultFailure<None>.CreateMessageResult(new None(), result.GetMessages());
        }

        await _uow.SaveChangesAsync();

        return ResultSuccess<None>.CreateMessageResult(new None(), ["Availability interval added successfully."]);
    }
}