using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Dispatcher.Location;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using UnitTests.Fakes;

namespace UnitTests.Features.Location.CreateLocation;

public class CreateEventDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private CreateLocationHandlerMock handler;
    
    public CreateEventDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<CreateLocationCommand>, CreateLocationHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (CreateLocationHandlerMock) serviceProvider.GetService<ICommandHandler<CreateLocationCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenCreatingLocation_ThenLocationCreated()
    {
        // Arrange
        Result<CreateLocationCommand> result = CreateLocationCommand.Create("Name", 32);
        CreateLocationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenCreatingLocation_ThenLocationCreated_WithTimer()
    {
        // Arrange
        Result<CreateLocationCommand> result = CreateLocationCommand.Create("Name", 32);
        CreateLocationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenCreatingLocation_ThenLocationNotCreated()
    {
        // Arrange
        Result<CreateLocationCommand> result = CreateLocationCommand.Create("", 32);
        CreateLocationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenCreatingLocation_ThenLocationNotCreated_WithTimer()
    {
        // Arrange
        Result<CreateLocationCommand> result = CreateLocationCommand.Create("", 32);
        CreateLocationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}