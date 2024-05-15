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

namespace UnitTests.Features.Guest.Participate;

public class AddParticipationDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private AddParticipationHandlerMock handler;
    
    public AddParticipationDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<AddParticipationCommand>, AddParticipationHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (AddParticipationHandlerMock) serviceProvider.GetService<ICommandHandler<AddParticipationCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenAddingParticipation_ThenParticipationAdded()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", Guid.NewGuid());
        AddParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenAddingParticipation_ThenParticipationAdded_WithTimer()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", Guid.NewGuid());
        AddParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAddingParticipation_ThenParticipationNotAdded()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("", Guid.NewGuid());
        AddParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAddingParticipation_ThenParticipationNotAdded_WithTimer()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("", Guid.NewGuid());
        AddParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenAddingParticipation_ThenParticipationNotAdded()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", Guid.Empty);
        AddParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidEventId_WhenAddingParticipation_ThenParticipationNotAdded_WithTimer()
    {
        // Arrange
        Result<AddParticipationCommand> result = AddParticipationCommand.Create("Guest1@example.com", Guid.Empty);
        AddParticipationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}