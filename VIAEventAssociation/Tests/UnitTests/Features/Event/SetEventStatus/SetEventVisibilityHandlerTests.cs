using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using UnitTests.Features.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class SetEventStatusHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<SetEventStatusCommand> _handler;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public SetEventStatusHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new SetEventStatusHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenStatusReady_WhenSettingStatus_ThenStatusSet()
    {
        // Arrange
        Result<SetEventStatusCommand> statusCommand = SetEventStatusCommand.Create(1, "Ready");

        // Act
        if (statusCommand.IsFailure())
        {
            foreach (var error in statusCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(statusCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenStatusActive_WhenSettingStatus_ThenStatusSet()
    {
        // Arrange
        Result<SetEventStatusCommand> statusCommand = SetEventStatusCommand.Create(1, "Active");

        // Act
        if (statusCommand.IsFailure())
        {
            foreach (var error in statusCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(statusCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenStatusCancelled_WhenSettingStatus_ThenStatusSet()
    {
        // Arrange
        Result<SetEventStatusCommand> statusCommand = SetEventStatusCommand.Create(1, "Cancelled");

        // Act
        if (statusCommand.IsFailure())
        {
            foreach (var error in statusCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(statusCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenStatusDeleted_WhenSettingStatus_ThenStatusSet()
    {
        // Arrange
        Result<SetEventStatusCommand> statusCommand = SetEventStatusCommand.Create(1, "Deleted");

        // Act
        if (statusCommand.IsFailure())
        {
            foreach (var error in statusCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(statusCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }

    [Fact]
    public async Task GivenInvalidData_WhenSettingStatus_ThenStatusNotSet()
    {
        // Arrange
        Result<SetEventStatusCommand> statusCommand = SetEventStatusCommand.Create(1, "InvalidStatus");

        // Act
        if (statusCommand.IsFailure())
        {
            foreach (var error in statusCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(statusCommand.GetObj());

        // Assert
        Assert.True(result.IsFailure());
    }
}