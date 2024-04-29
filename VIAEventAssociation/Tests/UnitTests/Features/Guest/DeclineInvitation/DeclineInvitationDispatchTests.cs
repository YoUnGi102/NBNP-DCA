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

namespace UnitTests.Features.Guest.DeclineInvitation;

public class DeclineInvitationDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private DeclineInvitationHandlerMock handler;
    
    public DeclineInvitationDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<DeclineInvitationCommand>, DeclineInvitationHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (DeclineInvitationHandlerMock) serviceProvider.GetService<ICommandHandler<DeclineInvitationCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenDecliningInvitation_ThenInvitationDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        DeclineInvitationCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenDecliningInvitation_ThenInvitationDeclined_WithTimer()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        DeclineInvitationCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }

    [Fact]
    public async Task GivenEmptyEmail_WhenDecliningInvitation_ThenInvitationNotDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        DeclineInvitationCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenEmptyEmail_WhenDecliningInvitation_ThenInvitationNotDeclined_WithTimer()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        DeclineInvitationCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }

    [Fact]
    public async Task GivenInvalidEventId_WhenDecliningInvitation_ThenInvitationNotDeclined()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", "");
        DeclineInvitationCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidEventId_WhenDecliningInvitation_ThenInvitationNotDeclined_WithTimer()
    {
        // Arrange
        Result<DeclineInvitationCommand> result = DeclineInvitationCommand.Create("Guest1@example.com", "");
        DeclineInvitationCommand command = result.GetObj();

        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}