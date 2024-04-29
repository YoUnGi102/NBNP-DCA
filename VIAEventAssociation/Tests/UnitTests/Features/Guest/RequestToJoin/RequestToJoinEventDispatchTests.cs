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
        string validGuestId = "e2399bcd-b83b-400f-bfba-2e58cb2b2330";
        string validEventId = "3b1d8789-e982-41b4-9f77-a7459fd6f51e";

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
        string validGuestId = "e2399bcd-b83b-400f-bfba-2e58cb2b2330";
        string validEventId = "3b1d8789-e982-41b4-9f77-a7459fd6f51e";

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
        string invalidGuestId = "";
        string invalidEventId = "";

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
        string invalidGuestId = "";
        string invalidEventId = "";

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