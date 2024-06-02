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

public class SetEventVisibilityHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<SetEventVisibilityCommand> _handler;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public SetEventVisibilityHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new SetEventVisibilityHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenSettingVisibility_ThenVisibilitySet()
    {
        // Arrange
        Result<SetEventVisibilityCommand> visibilityCommand = SetEventVisibilityCommand.Create(new Guid(), "Public");

        // Act
        if (visibilityCommand.IsFailure())
        {
            foreach (var error in visibilityCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(visibilityCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenValidData2_WhenSettingVisibility_ThenVisibilitySet()
    {
        // Arrange
        Result<SetEventVisibilityCommand> visibilityCommand = 
            SetEventVisibilityCommand.Create(new Guid(), "Private");

        // Act
        if (visibilityCommand.IsFailure())
        {
            foreach (var error in visibilityCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(visibilityCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }

    [Fact]
    public async Task GivenInvalidData_WhenSettingVisibility_ThenVisibilityNotSet()
    {
        // Arrange
        Result<SetEventVisibilityCommand> visibilityCommand = 
            SetEventVisibilityCommand.Create(new Guid(), "InvalidVisibility");

        // Act
        if (visibilityCommand.IsFailure())
        {
            foreach (var error in visibilityCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(visibilityCommand.GetObj());

        // Assert
        Assert.True(result.IsFailure());
    }
}