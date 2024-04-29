﻿using System;
using Domain.Common.Helpers;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using Constants = UnitTests.Fakes.Constants;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventStartDateTimeCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenUpdatingStartDateTime_ThenStartDateTimeUpdated()
    {
        // Arrange
        Result<UpdateEventStartDateTimeCommand> result = UpdateEventStartDateTimeCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", DateParser.ToString(DateTime.Now.AddDays(2)));
        UpdateEventStartDateTimeCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenPastDate_WhenUpdatingStartDateTime_ThenStartDateTimeNotUpdated()
    {
        // Arrange
        Result<UpdateEventStartDateTimeCommand> result = UpdateEventStartDateTimeCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", DateParser.ToString(DateTime.Now.AddDays(-1)));
        UpdateEventStartDateTimeCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}