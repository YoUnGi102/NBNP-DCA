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

namespace UnitTests.Features.Event.UpdateEndDateTime;

public class UpdateEventEndDateTimeDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private UpdateEventEndDateTimeHandlerMock handler;
    
    public UpdateEventEndDateTimeDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<UpdateEventEndDateTimeCommand>, UpdateEventEndDateTimeHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (UpdateEventEndDateTimeHandlerMock) serviceProvider.GetService<ICommandHandler<UpdateEventEndDateTimeCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingEventEndDateTime_ThenEventEndDateTimeUpdated()
    {
        // Arrange
        Result<UpdateEventEndDateTimeCommand> result = UpdateEventEndDateTimeCommand.Create(
            1, 
            DateTime.Now.ToString());
        UpdateEventEndDateTimeCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingEventEndDateTime_ThenEventEndDateTimeUpdated_WithTimer()
    {
        // Arrange
        Result<UpdateEventEndDateTimeCommand> result = UpdateEventEndDateTimeCommand.Create(
            1, 
            DateTime.Now.ToString());
        UpdateEventEndDateTimeCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}