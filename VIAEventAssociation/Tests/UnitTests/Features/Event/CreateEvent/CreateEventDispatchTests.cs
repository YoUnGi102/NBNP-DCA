using Microsoft.Extensions.DependencyInjection;
using UnitTests.Fakes;
using UnitTests.Fakes.Moks.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private CreateEventHandlerMock handler;
    
    public CreateEventDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (CreateEventHandlerMock) serviceProvider.GetService<ICommandHandler<CreateEventCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenCreatingEvent_ThenEventCreated()
    {
        // Arrange
        Result<CreateEventCommand> result = CreateEventCommand.Create(
            "Chess Tournament", 
            "Monthly Chess Tournament by VIA Chess Club", 
            Constants.START_DATE_STRING,
            Constants.END_DATE_STRING, 
            100, 
            "Public",
            "Active", 
            1);
        CreateEventCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenCreatingEvent_ThenEventCreated_WithTimer()
    {
        // Arrange
        Result<CreateEventCommand> result = CreateEventCommand.Create(
            "Chess Tournament", 
            "Monthly Chess Tournament by VIA Chess Club", 
            Constants.START_DATE_STRING,
            Constants.END_DATE_STRING, 
            100, 
            "Public",
            "Active", 
            1);
        CreateEventCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenShortTitle_WhenCreatingEvent_ThenEventCreated()
    {
        // Arrange
        Result<CreateEventCommand> result = CreateEventCommand.Create(
            "1", 
            "Monthly Chess Tournament by VIA Chess Club", 
            Constants.START_DATE_STRING,
            Constants.END_DATE_STRING, 
            100, 
            "Visible",
            "Active", 
            1);
        CreateEventCommand? command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenShortTitle_WhenCreatingEvent_ThenEventCreated_WithTimer()
    {
        // Arrange
        Result<CreateEventCommand> result = CreateEventCommand.Create(
            "1", 
            "Monthly Chess Tournament by VIA Chess Club", 
            Constants.START_DATE_STRING,
            Constants.END_DATE_STRING, 
            100, 
            "Visible",
            "Active", 
            1);
        CreateEventCommand? command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}