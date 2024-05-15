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

namespace UnitTests.Features.Event.SetMaxGuests;

public class SetEventMaxGuestsDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private SetEventMaxGuestsHandlerMock handler;
    
    public SetEventMaxGuestsDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<SetEventMaxGuestsCommand>, SetEventMaxGuestsHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (SetEventMaxGuestsHandlerMock) serviceProvider.GetService<ICommandHandler<SetEventMaxGuestsCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingEventMaxGuests_ThenEventMaxGuestsSet()
    {
        // Arrange
        Result<SetEventMaxGuestsCommand> result = SetEventMaxGuestsCommand.Create(
            new Guid(), 
            100);
        SetEventMaxGuestsCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenSettingEventMaxGuests_ThenEventMaxGuestsSet_WithTimer()
    {
        // Arrange
        Result<SetEventMaxGuestsCommand> result = SetEventMaxGuestsCommand.Create(
            new Guid(), 
            100);
        SetEventMaxGuestsCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenSettingEventMaxGuests_ThenEventMaxGuestsNotSet()
    {
        // Arrange
        Result<SetEventMaxGuestsCommand> result = SetEventMaxGuestsCommand.Create(
            new Guid(), 
            -1);
        SetEventMaxGuestsCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}