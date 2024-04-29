﻿using Microsoft.Extensions.DependencyInjection;
using UnitTests.Fakes.Moks.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventTitleDispatchTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ICommandDispatcher _commandDispatcherWTimer;
    private UpdateEventTitleHandlerMock handler;
    
    public UpdateEventTitleDispatchTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<UpdateEventTitleCommand>, UpdateEventTitleHandlerMock>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(serviceProvider);
        _commandDispatcherWTimer = new CommandDispatcherWithTimer(_commandDispatcher);
        handler = (UpdateEventTitleHandlerMock) serviceProvider.GetService<ICommandHandler<UpdateEventTitleCommand>>()!;
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingEventTitle_ThenEventTitleUpdated()
    {
        // Arrange
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e", 
            "New Title");
        UpdateEventTitleCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenValidData_WhenUpdatingEventTitle_ThenEventTitleUpdated_WithTimer()
    {
        // Arrange
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e", 
            "New Title");
        UpdateEventTitleCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenUpdatingEventTitle_ThenEventTitleNotUpdated()
    {
        // Arrange
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e", 
            "");
        UpdateEventTitleCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcher.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenUpdatingEventTitle_ThenEventTitleNotUpdated_WithTimer()
    {
        // Arrange
        Result<UpdateEventTitleCommand> result = UpdateEventTitleCommand.Create(
            "3b1d8789-e982-41b4-9f77-a7459fd6f51e", 
            "");
        UpdateEventTitleCommand command = result.GetObj()!;
        
        // Act
        Result<None> dispatchResult = await _commandDispatcherWTimer.DispatchAsync(command);
        
        // Assert
        _testOutputHelper.WriteLine(handler.ReachedHere().ToString());
        Assert.True(handler.ReachedHere());
    }
}