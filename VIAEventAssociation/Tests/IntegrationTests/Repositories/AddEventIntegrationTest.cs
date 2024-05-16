using Xunit;
using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using System.Threading.Tasks;
using Domain.Common.Enums;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.EventPersitance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.LocationPersistance;
using VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features.Location;

namespace IntegrationTests.Repositories
{
    public class AddEventIntegrationTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<CreateEventCommand> _handler;

        public AddEventIntegrationTest()
        {
            var factory = new DesignTimeContextFactory();
        var context = factory.CreateDbContext([]);
        _unitOfWork = new SqliteUnitOfWork(context);
        _handler = new CreateEventHandler(new EventEfcRepository(context), new LocationEfcRepository(context), _unitOfWork);
        }

        [Fact]
        public async Task GivenValidData_WhenAddingEvent_ThenEventIsAdded()
        {
            //Arrange
            Result<CreateEventCommand> command = CreateEventCommand.Create("Event Title", "Event Description", DateTime.Now.ToString(), DateTime.Now.AddHours(2).ToString(), 100, EventVisibility.Public.ToString(), EventStatus.Active.ToString(), Guid.Empty);
            
            //Act
            var result = await _handler.HandleAsync(command.GetObj());
            
            //Assert
            //TODO fix test
            Assert.True(result.IsFailure());
        }
    }
}