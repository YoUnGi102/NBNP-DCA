using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.LocationPersistance;

public class LocationEfcRepository(DmContext context) : BaseEfcRepository<Domain.Aggregates.Locations.Location>(context), ILocationRepository
{
    private DmContext context = context;
}