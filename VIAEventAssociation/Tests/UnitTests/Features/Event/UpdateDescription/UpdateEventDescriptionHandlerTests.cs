using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using UnitTests.Features.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class UpdateEventDescriptionHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<UpdateEventDescriptionCommand> _handler;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public UpdateEventDescriptionHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new UpdateEventDescriptionHandler(_repository, _uow);
   }

    [Fact]
    public async Task GivenValidData_WhenUpdatingDescription_ThenDescriptionUpdated()
    {
        // Arrange
        Result<UpdateEventDescriptionCommand> descriptionCommand = UpdateEventDescriptionCommand.Create(1, "New Description");

        // Act
        if (descriptionCommand.IsFailure())
        {
            foreach (var error in descriptionCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(descriptionCommand.GetObj());
        
        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        
        // Assert
        Assert.False(result.IsFailure());
        
    }
    
    [Fact]
    public async Task GivenShortDescription_WhenUpdatingDescription_ThenDescriptionNotUpdated()
    {
        // Arrange
        Result<UpdateEventDescriptionCommand> descriptionCommand = UpdateEventDescriptionCommand.Create(1, "");

        // Act
        if (descriptionCommand.IsFailure())
        {
            foreach (var error in descriptionCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(descriptionCommand.GetObj());
        
        // Assert
        Assert.True(result.IsFailure());
    }
    
}