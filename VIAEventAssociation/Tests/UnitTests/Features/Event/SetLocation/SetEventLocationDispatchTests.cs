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

namespace UnitTests.Features.Event.SetLocation;

public class SetEventLocationDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private SetEventLocationHandlerMock handler;
    
    public SetEventLocationDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<SetEventLocationCommand>, SetEventLocationHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (SetEventLocationHandlerMock) serviceProvider.GetService<ICommandHandler<SetEventLocationCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingEventLocation_ThenEventLocationSet()
    {
        // Arrange
        Result<SetEventLocationCommand> result = SetEventLocationCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e", 
            "7c59adac-5a10-4de9-8783-ea2add07bb65");
        SetEventLocationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingEventLocation_ThenEventLocationSet_WithTimer()
    {
        // Arrange
        Result<SetEventLocationCommand> result = SetEventLocationCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e", 
            "7c59adac-5a10-4de9-8783-ea2add07bb65");
        SetEventLocationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingEventLocation_ThenEventLocationNotSet()
    {
        // Arrange
        Result<SetEventLocationCommand> result = SetEventLocationCommand.Create(
            "", 
            "");
        SetEventLocationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}