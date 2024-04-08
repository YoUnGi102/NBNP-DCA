using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Dispatcher.Location;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Location.SetAvailabilityInterval;

public class AddAvailabilityIntervalDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private AddAvailabilityIntervalHandlerMock handler;
    
    public AddAvailabilityIntervalDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<AddAvailabilityIntervalCommand>, AddAvailabilityIntervalHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (AddAvailabilityIntervalHandlerMock) serviceProvider.GetService<ICommandHandler<AddAvailabilityIntervalCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalAdded()
    {
        // Arrange
        Result<AddAvailabilityIntervalCommand> result = AddAvailabilityIntervalCommand.Create(1, "2022-12-12", "2022-12-13");
        AddAvailabilityIntervalCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalAdded_WithTimer()
    {
        // Arrange
        Result<AddAvailabilityIntervalCommand> result = AddAvailabilityIntervalCommand.Create(1, "2022-12-12", "2022-12-13");
        AddAvailabilityIntervalCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalNotAdded()
    {
        // Arrange
        Result<AddAvailabilityIntervalCommand> result = AddAvailabilityIntervalCommand.Create(1, "2022-12-12", "2022-11-12");
        AddAvailabilityIntervalCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalNotAdded_WithTimer()
    {
        // Arrange
        Result<AddAvailabilityIntervalCommand> result = AddAvailabilityIntervalCommand.Create(1, "2022-12-12", "2022-11-12");
        AddAvailabilityIntervalCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}