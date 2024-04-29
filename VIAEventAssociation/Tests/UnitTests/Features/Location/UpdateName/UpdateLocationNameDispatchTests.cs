using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Dispatcher.Location;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Location.UpdateName;

public class UpdateLocationNameDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private UpdateLocationNameHandlerMock handler;
    
    public UpdateLocationNameDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<UpdateLocationNameCommand>, UpdateLocationNameHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (UpdateLocationNameHandlerMock) serviceProvider.GetService<ICommandHandler<UpdateLocationNameCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingLocationName_ThenLocationNameUpdated()
    {
        // Arrange
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", "Name");
        UpdateLocationNameCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingLocationName_ThenLocationNameUpdated_WithTimer()
    {
        // Arrange
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", "Name");
        UpdateLocationNameCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenUpdatingLocationName_ThenLocationNameNotUpdated()
    {
        // Arrange
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", "");
        UpdateLocationNameCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenUpdatingLocationName_ThenLocationNameNotUpdated_WithTimer()
    {
        // Arrange
        Result<UpdateLocationNameCommand> result = UpdateLocationNameCommand.Create("7c59adac-5a10-4de9-8783-ea2add07bb65", "");
        UpdateLocationNameCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
    }
}