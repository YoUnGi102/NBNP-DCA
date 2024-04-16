using Domain.Common.UnitOfWork;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.LocationPersistance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using ViaEventAssociation.Core.Application.Features.Location;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories;

public class AddLocationIntegrationTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommandHandler<CreateLocationCommand> _handler;

    public AddLocationIntegrationTest()
    {
        var factory = new DesignTimeContextFactory();
        var context = factory.CreateDbContext([]);
        _unitOfWork = new SqliteUnitOfWork(context);
        _handler = new CreateLocationHandler(new LocationEfcRepository(context), _unitOfWork);
    }

    [Fact]
    public async Task GivenValidData_WhenAddingLocation_ThenLocationIsAdded()
    {
        //Arrange
        var res = CreateLocationCommand.Create("VIA University College", 100);
        
        //Act
        var result = await _handler.HandleAsync(res.GetObj());

        //Assert
        Assert.False(result.IsFailure());
    }
}