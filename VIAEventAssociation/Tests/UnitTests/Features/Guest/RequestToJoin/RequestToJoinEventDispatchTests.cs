using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Dispatcher.Guest;
using UnitTests.Fakes;
using UnitTests.Fakes.Moks.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Guest.RequestToJoin;

public class RequestToJoinEventDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private RequestToJoinEventHandlerMock handler;
    
    public RequestToJoinEventDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<RequestJoinEventCommand>, RequestToJoinEventHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (RequestToJoinEventHandlerMock) serviceProvider.GetService<ICommandHandler<RequestJoinEventCommand>>()!;
    }
    
    [Fact]
    public async void GivenValidData_WhenCreatingCommand_ThenCommandCreated()
    {
        // Arrange
        Guid validGuestId = Guid.NewGuid();
        Guid validEventId = Guid.NewGuid();

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(validGuestId, validEventId);
        RequestJoinEventCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async void GivenValidData_WhenCreatingCommand_ThenCommandCreated_WithTimer()
    {
        // Arrange
        Guid validGuestId = Guid.NewGuid();
        Guid validEventId = Guid.NewGuid();

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(validGuestId, validEventId);
        RequestJoinEventCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }

    [Fact]
    public async void GivenInvalidData_WhenCreatingCommand_ThenCommandNotCreated()
    {
        // Arrange
        Guid invalidGuestId = Guid.Empty;
        Guid invalidEventId = Guid.Empty;

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(invalidGuestId, invalidEventId);
        RequestJoinEventCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async void GivenInvalidData_WhenCreatingCommand_ThenCommandNotCreated_WithTimer()
    {
        // Arrange
        Guid invalidGuestId = Guid.Empty;
        Guid invalidEventId = Guid.Empty;

        // Act
        Result<RequestJoinEventCommand> result = RequestJoinEventCommand.Create(invalidGuestId, invalidEventId);
        RequestJoinEventCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}