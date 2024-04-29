using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Dispatcher.Location;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Location.SetMaxCapacity;

public class SetLocationMaxCapacityDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private UpdateLocationMaxCapacityHandlerMock handler;
    
    public SetLocationMaxCapacityDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<UpdateLocationMaxCapacityCommand>, UpdateLocationMaxCapacityHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (UpdateLocationMaxCapacityHandlerMock) serviceProvider.GetService<ICommandHandler<UpdateLocationMaxCapacityCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingMaxCapacity_ThenMaxCapacitySet()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> result = UpdateLocationMaxCapacityCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", 32);
        UpdateLocationMaxCapacityCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingMaxCapacity_ThenMaxCapacitySet_WithTimer()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> result = UpdateLocationMaxCapacityCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", 32);
        UpdateLocationMaxCapacityCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingMaxCapacity_ThenMaxCapacityNotSet()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> result = UpdateLocationMaxCapacityCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", -1);
        UpdateLocationMaxCapacityCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingMaxCapacity_ThenMaxCapacityNotSet_WithTimer()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> result = UpdateLocationMaxCapacityCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", -1);
        UpdateLocationMaxCapacityCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}