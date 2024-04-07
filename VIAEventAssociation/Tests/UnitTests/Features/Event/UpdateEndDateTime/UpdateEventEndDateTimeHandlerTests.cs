using Domain.Aggregates.Events;
using Domain.Common.Helpers;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventStartDateTimeHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateEventStartDateTimeCommand> _handler;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public UpdateEventStartDateTimeHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new UpdateEventStartDateTimeHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenUpdatingStartDateTime_ThenStartDateTimeUpdated()
    {
        // Arrange
        Result<UpdateEventStartDateTimeCommand> startDateTimeCommand = UpdateEventStartDateTimeCommand.Create(1, DateParser.ToString(DateTime.Now.AddDays(2)));

        // Act
        if (startDateTimeCommand.IsFailure())
        {
            foreach (var error in startDateTimeCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(startDateTimeCommand.GetObj());
        
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        
        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenPastDate_WhenUpdatingStartDateTime_ThenStartDateTimeNotUpdated()
    {
        // Arrange
        Result<UpdateEventStartDateTimeCommand> startDateTimeCommand = UpdateEventStartDateTimeCommand.Create(1, DateParser.ToString(DateTime.Now.AddDays(-1)));

        // Act
        if (startDateTimeCommand.IsFailure())
        {
            foreach (var error in startDateTimeCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(startDateTimeCommand.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
}