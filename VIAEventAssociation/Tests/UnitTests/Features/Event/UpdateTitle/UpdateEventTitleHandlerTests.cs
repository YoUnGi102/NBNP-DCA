using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using UnitTests.Features.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventTitleHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateEventTitleCommand> _handler;
    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public UpdateEventTitleHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new UpdateEventTitleHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenUpdatingTitle_ThenTitleUpdated()
    {
        // Arrange
        Result<UpdateEventTitleCommand> titleCommand = UpdateEventTitleCommand.Create(new Guid(), "New Title");

        // Act
        if (titleCommand.IsFailure())
        {
            foreach (var error in titleCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(titleCommand.GetObj());
        
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        
        // Assert
        Assert.False(result.IsFailure());
        
    }
    
    [Fact]
    public async Task GivenShortTitle_WhenUpdatingTitle_ThenTitleNotUpdated()
    {
        // Arrange
        Result<UpdateEventTitleCommand> titleCommand = UpdateEventTitleCommand.Create(new Guid(), "");

        // Act
        if (titleCommand.IsFailure())
        {
            foreach (var error in titleCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(titleCommand.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
    
}