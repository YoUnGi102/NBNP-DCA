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

public class UpdateLocationNameHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateLocationNameCommand> _handler;
    private ILocationRepository _repository;
    private IUnitOfWork _uow;

    public UpdateLocationNameHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new LocationRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new UpdateLocationNameHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenUpdatingName_ThenNameUpdated()
    {
        // Arrange
        Result<UpdateLocationNameCommand> nameCommand = UpdateLocationNameCommand.Create(Guid.Empty, "New Name");

        // Act
        if (nameCommand.IsFailure())
        {
            foreach (var error in nameCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        Result<None> result = await _handler.HandleAsync(nameCommand.GetObj());
        
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        
        // Assert
        Assert.False(result.IsFailure());
        
    }
    
    [Fact]
    public async Task GivenShortName_WhenUpdatingName_ThenNameNotUpdated()
    {
        // Arrange
        Result<UpdateLocationNameCommand> nameCommand = UpdateLocationNameCommand.Create(Guid.NewGuid(), "");

        // Act
        if (nameCommand.IsFailure())
        {
            foreach (var error in nameCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(nameCommand.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
    
}