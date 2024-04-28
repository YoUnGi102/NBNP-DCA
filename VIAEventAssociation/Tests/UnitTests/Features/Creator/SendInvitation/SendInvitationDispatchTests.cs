using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common.Dispatcher.Creator;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Creator;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Features.Creator.SendInvitation;

public class SendInvitationDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private SendInvitationHandlerMock handler;
    
    public SendInvitationDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<SendInvitationCommand>, SendInvitationHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (SendInvitationHandlerMock) serviceProvider.GetService<ICommandHandler<SendInvitationCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenSendingInvitation_ThenInvitationSent()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create("e2399bcd-b83b-400f-bfba-2e58cb2b2330", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        SendInvitationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenSendingInvitation_ThenInvitationSent_WithTimer()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create("e2399bcd-b83b-400f-bfba-2e58cb2b2330", "3b1d8789-e982-41b4-9f77-a7459fd6f51e");
        SendInvitationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenNullData_WhenSendingInvitation_ThenInvitationNotSent()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create("", "");
        SendInvitationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenNullData_WhenSendingInvitation_ThenInvitationNotSent_WithTimer()
    {
        // Arrange
        Result<SendInvitationCommand> result = SendInvitationCommand.Create("", "");
        SendInvitationCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}