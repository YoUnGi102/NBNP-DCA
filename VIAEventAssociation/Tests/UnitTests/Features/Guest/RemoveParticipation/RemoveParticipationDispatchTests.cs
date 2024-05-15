using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Dispatcher.Guest;
using UnitTests.Fakes;
using UnitTests.Fakes.Moks.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Guest.RemoveParticipation;

public class RemoveParticipationDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private RemoveParticipationHandlerMock handler;
    
    public RemoveParticipationDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<RemoveParticipationCommand>, RemoveParticipationHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (RemoveParticipationHandlerMock) serviceProvider.GetService<ICommandHandler<RemoveParticipationCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenRemovingParticipation_ThenParticipationRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("guest1@gmail.com", Guid.NewGuid());
        RemoveParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenRemovingParticipation_ThenParticipationRemoved_WithTimer()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("guest1@gmail.com", Guid.NewGuid());
        RemoveParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenRemovingParticipation_ThenParticipationNotRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("", Guid.NewGuid());
        RemoveParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenRemovingParticipation_ThenParticipationNotRemoved_WithTimer()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("", Guid.NewGuid());
        RemoveParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenRemovingParticipation_ThenParticipationNotRemoved()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("Guest1@example.com", Guid.Empty);
        RemoveParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidEventId_WhenRemovingParticipation_ThenParticipationNotRemoved_WithTimer()
    {
        // Arrange
        Result<RemoveParticipationCommand> result = RemoveParticipationCommand.Create("Guest1@example.com", Guid.Empty);
        RemoveParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}