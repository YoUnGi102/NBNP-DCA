using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;
using System;
using Domain.Common.Helpers;
using ViaEventAssociation.Core.Application.Features.Location;

namespace VIAEventAssociation.Tests.UnitTests.Features.Location.Update;

public class AddAvailabilityIntervalHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<AddAvailabilityIntervalCommand> _handler;
    private ILocationRepository _repository;
    private IUnitOfWork _uow;

    public AddAvailabilityIntervalHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new LocationRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new AddAvailabilityIntervalHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalAdded()
    {
        // Arrange
        string startDate = DateParser.ToString(DateTime.Now);
        string endDate = DateParser.ToString(DateTime.Now.AddDays(1));
        Result<AddAvailabilityIntervalCommand> intervalCommand = AddAvailabilityIntervalCommand.Create(Guid.NewGuid(), startDate, endDate);

        // Act
        if (intervalCommand.IsFailure())
        {
            foreach (var error in intervalCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(intervalCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.True(result.IsFailure());
    }

    [Fact]
    public async Task GivenInvalidData_WhenAddingAvailabilityInterval_ThenAvailabilityIntervalNotAdded()
    {
        // Arrange
        string startDate = DateParser.ToString(DateTime.Now);
        string endDate = DateParser.ToString(DateTime.Now.AddDays(-1)); // End date is before start date
        Result<AddAvailabilityIntervalCommand> intervalCommand = AddAvailabilityIntervalCommand.Create(Guid.NewGuid(), startDate, endDate);

        // Act
        if (intervalCommand.IsFailure())
        {
            foreach (var error in intervalCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(intervalCommand.GetObj());

        // Assert
        Assert.True(result.IsFailure());
    }
}