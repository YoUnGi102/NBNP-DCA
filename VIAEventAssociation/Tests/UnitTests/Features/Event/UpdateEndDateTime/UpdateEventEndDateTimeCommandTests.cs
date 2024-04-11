using System;
using Domain.Common.Helpers;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using Constants = UnitTests.Fakes.Constants;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventEndDateTimeCommandTests
{

    [Fact]
    public async Task GivenValidData_WhenUpdatingEndDateTime_ThenEndDateTimeUpdated()
    {
        // Arrange
        Result<UpdateEventEndDateTimeCommand> result = UpdateEventEndDateTimeCommand.Create(1, DateParser.ToString(DateTime.Now.AddDays(2)));
        UpdateEventEndDateTimeCommand command = result.GetObj();
        
        // Assert
        Assert.False(result.IsFailure());
        Assert.NotNull(command);
        Assert.NotNull(command.Id.ToString());
    }
    
    [Fact]
    public async Task GivenPastDate_WhenUpdatingEndDateTime_ThenEndDateTimeNotUpdated()
    {
        // Arrange
        Result<UpdateEventEndDateTimeCommand> result = UpdateEventEndDateTimeCommand.Create(1, DateParser.ToString(DateTime.Now.AddDays(-1)));
        UpdateEventEndDateTimeCommand command = result.GetObj();
        
        // Assert
        Assert.True(result.IsFailure());
        Assert.Null(command);
    }
    
}