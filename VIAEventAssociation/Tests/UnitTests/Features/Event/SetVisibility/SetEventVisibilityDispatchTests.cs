using Microsoft.Extensions.DependencyInjection;
using UnitTests.Fakes.Moks.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.SetVisibility;

public class SetEventVisibilityDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private SetEventVisibilityHandlerMock handler;
    
    public SetEventVisibilityDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<SetEventVisibilityCommand>, SetEventVisibilityHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (SetEventVisibilityHandlerMock) serviceProvider.GetService<ICommandHandler<SetEventVisibilityCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingEventVisibility_ThenEventVisibilitySet()
    {
        // Arrange
        Result<SetEventVisibilityCommand> result = SetEventVisibilityCommand.Create(
            1, 
            "Public");
        SetEventVisibilityCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingEventVisibility_ThenEventVisibilitySet_WithTimer()
    {
        // Arrange
        Result<SetEventVisibilityCommand> result = SetEventVisibilityCommand.Create(
            1, 
            "Public");
        SetEventVisibilityCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
}