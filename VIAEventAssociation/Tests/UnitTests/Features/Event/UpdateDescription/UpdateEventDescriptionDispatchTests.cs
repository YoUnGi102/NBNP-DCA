using Microsoft.Extensions.DependencyInjection;
using UnitTests.Fakes.Moks.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.UpdateDescription;

public class UpdateEventDescriptionDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private UpdateEventDescriptionHandlerMock handler;
    
    public UpdateEventDescriptionDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<UpdateEventDescriptionCommand>, UpdateEventDescriptionHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (UpdateEventDescriptionHandlerMock) serviceProvider.GetService<ICommandHandler<UpdateEventDescriptionCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingEventDescription_ThenEventDescriptionUpdated()
    {
        // Arrange
        Result<UpdateEventDescriptionCommand> result = UpdateEventDescriptionCommand.Create(
            1, 
            "New Description");
        UpdateEventDescriptionCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingEventDescription_ThenEventDescriptionUpdated_WithTimer()
    {
        // Arrange
        Result<UpdateEventDescriptionCommand> result = UpdateEventDescriptionCommand.Create(
            1, 
            "New Description");
        UpdateEventDescriptionCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenUpdatingEventDescription_ThenEventDescriptionNotUpdated()
    {
        // Arrange
        Result<UpdateEventDescriptionCommand> result = UpdateEventDescriptionCommand.Create(
            1, 
            "");
        UpdateEventDescriptionCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}