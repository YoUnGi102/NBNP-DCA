using Domain.Aggregates.Events;
using Domain.Aggregates.Locations;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using ViaEventAssociation.Core.Application.Features.Location;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Location.Create;

public class CreateLocationHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<CreateLocationCommand> _handler;

    private ILocationRepository _locationRepository;
    private IUnitOfWork _uow;

    public CreateLocationHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _locationRepository = new LocationRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new CreateLocationHandler(_locationRepository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenCreatingLocation_ThenLocationCreated()
    {
        // Arrange
        Result<CreateLocationCommand> cmd = CreateLocationCommand.Create("Chess Club", 100);

        // Act
        var result = await _handler.HandleAsync(cmd.GetObj());
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }
    
    [Fact]
    public async Task GivenInvalidData_WhenCreatingLocation_ThenLocationNotCreated()
    {
        // Arrange
        Result<CreateLocationCommand> cmd = CreateLocationCommand.Create("", 100);

        // Act
        if (cmd.IsFailure())
        {
            foreach (var error in cmd.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(cmd.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
}