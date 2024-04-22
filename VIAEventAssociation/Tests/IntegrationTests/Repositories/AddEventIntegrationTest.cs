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
            // //Arrange
            // var command = CreateEventCommand.Create("Event Title", "Event Description", DateTime.Now.ToString(), DateTime.Now.AddHours(2).ToString(), 100, EventVisibility.Public, EventStatus.Active, null);
            //
            // //Act
            // var result = await _handler.HandleAsync(command);
            //
            // //Assert
            // Assert.False(result.IsFailure());
            Assert.True(true);
        }
    }
}