using Xunit;
using Domain.Aggregates.Events;
using Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using System.Threading.Tasks;
using Domain.Common.Enums;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

namespace IntegrationTests.Repositories
{
    public class AddEventIntegrationTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<CreateEventCommand> _handler;

        public AddEventIntegrationTest(IUnitOfWork unitOfWork, ICommandHandler<CreateEventCommand> handler)
        {
            _unitOfWork = unitOfWork;
            _handler = handler;
        }

        [Fact]
        public async Task GivenValidData_WhenAddingEvent_ThenEventIsAdded()
        {
            // Arrange
            // var command = new CreateEventCommand("Event Title", "Event Description", DateTime.Now, DateTime.Now.AddHours(2), 100, EventVisibility.Public, EventStatus.Active, null, null);

            // Act
            // var result = await _handler.HandleAsync(command);

            // Assert
            // Assert.False(result.IsFailure());
        }
    }
}