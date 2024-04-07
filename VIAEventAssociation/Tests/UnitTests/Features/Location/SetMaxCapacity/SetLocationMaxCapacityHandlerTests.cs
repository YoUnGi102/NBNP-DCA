using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Location.Update;

public class UpdateLocationMaxCapacityHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateLocationMaxCapacityCommand> _handler;
    private ILocationRepository _repository;
    private IUnitOfWork _uow;

    public UpdateLocationMaxCapacityHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new LocationRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new UpdateLocationMaxCapacityHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenUpdatingMaxCapacity_ThenMaxCapacityUpdated()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> maxCapacityCommand = UpdateLocationMaxCapacityCommand.Create(1, 100);

        // Act
        if (maxCapacityCommand.IsFailure())
        {
            foreach (var error in maxCapacityCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(maxCapacityCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());

    }

    [Fact]
    public async Task GivenInvalidData_WhenUpdatingMaxCapacity_ThenMaxCapacityNotUpdated()
    {
        // Arrange
        Result<UpdateLocationMaxCapacityCommand> maxCapacityCommand = UpdateLocationMaxCapacityCommand.Create(1, -1);

        // Act
        if (maxCapacityCommand.IsFailure())
        {
            foreach (var error in maxCapacityCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(maxCapacityCommand.GetObj());

        // Assert
        Assert.True(result.IsFailure());
    }

}