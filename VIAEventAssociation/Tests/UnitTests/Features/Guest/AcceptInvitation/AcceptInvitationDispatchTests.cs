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

namespace UnitTests.Features.Guest.AcceptInvitation;

public class AcceptInvitationDispatchTests
{
    
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private AcceptInvitationHandlerMock handler;
    
    public AcceptInvitationDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<AcceptInvitationCommand>, AcceptInvitationHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (AcceptInvitationHandlerMock) serviceProvider.GetService<ICommandHandler<AcceptInvitationCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenAcceptingInvitation_ThenInvitationAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", 1);
        AcceptInvitationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenAcceptingInvitation_ThenInvitationAccepted_WithTimer()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", 1);
        AcceptInvitationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAcceptingInvitation_ThenInvitationNotAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("", 1);
        AcceptInvitationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenAcceptingInvitation_ThenInvitationNotAccepted_WithTimer()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("", 1);
        AcceptInvitationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenAcceptingInvitation_ThenInvitationNotAccepted()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", -1);
        AcceptInvitationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidEventId_WhenAcceptingInvitation_ThenInvitationNotAccepted_WithTimer()
    {
        // Arrange
        Result<AcceptInvitationCommand> result = AcceptInvitationCommand.Create("Guest1@example.com", -1);
        AcceptInvitationCommand command = result.GetObj();
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
}