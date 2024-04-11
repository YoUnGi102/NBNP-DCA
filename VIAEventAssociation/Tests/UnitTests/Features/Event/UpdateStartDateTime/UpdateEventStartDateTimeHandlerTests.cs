using Domain.Aggregates.Events;
using Domain.Common.Helpers;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventEndDateTimeHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateEventEndDateTimeCommand> _handler;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public UpdateEventEndDateTimeHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new UpdateEventEndDateTimeHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenUpdatingEndDateTime_ThenEndDateTimeUpdated()
    {
        // Arrange
        Result<UpdateEventEndDateTimeCommand> endDateTimeCommand = UpdateEventEndDateTimeCommand.Create(1, DateParser.ToString(DateTime.Now.AddDays(2)));

        // Act
        if (endDateTimeCommand.IsFailure())
        {
            foreach (var error in endDateTimeCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(endDateTimeCommand.GetObj());
        
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        
        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenPastDate_WhenUpdatingEndDateTime_ThenEndDateTimeNotUpdated()
    {
        // Arrange
        Result<UpdateEventEndDateTimeCommand> endDateTimeCommand = UpdateEventEndDateTimeCommand.Create(1, DateParser.ToString(DateTime.Now.AddDays(-1)));

        // Act
        if (endDateTimeCommand.IsFailure())
        {
            foreach (var error in endDateTimeCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(endDateTimeCommand.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
}