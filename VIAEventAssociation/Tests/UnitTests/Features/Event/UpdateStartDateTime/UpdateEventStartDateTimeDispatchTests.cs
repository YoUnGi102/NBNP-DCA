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

namespace UnitTests.Features.Event.UpdateStartDateTime;

public class UpdateEventStartDateTimeDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private UpdateEventStartDateTimeHandlerMock handler;
    
    public UpdateEventStartDateTimeDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<UpdateEventStartDateTimeCommand>, UpdateEventStartDateTimeHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (UpdateEventStartDateTimeHandlerMock) serviceProvider.GetService<ICommandHandler<UpdateEventStartDateTimeCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingEventStartDateTime_ThenEventStartDateTimeUpdated()
    {
        // Arrange
        Result<UpdateEventStartDateTimeCommand> result = UpdateEventStartDateTimeCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e", 
            DateTime.Now.ToString());
        UpdateEventStartDateTimeCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }

    [Fact]
    public async Task GivenValidData_WhenUpdatingEventStartDateTime_ThenEventStartDateTimeUpdated_WithTimer()
    {
        // Arrange
        Result<UpdateEventStartDateTimeCommand> result = UpdateEventStartDateTimeCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e",
            DateTime.Now.ToString());
        UpdateEventStartDateTimeCommand command = result.GetObj()!;

        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);

        // Assert
    }
}