using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using UnitTests.Features.Event;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Event.Activate;

public class SetEventMaxGuestsHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<SetEventMaxGuestsCommand> _handler;

    private IEventRepository _repository;
    private IUnitOfWork _uow;

    public SetEventMaxGuestsHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new EventRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new SetEventMaxGuestsHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenSettingMaxGuests_ThenMaxGuestsSet()
    {
        // Arrange
        Result<SetEventMaxGuestsCommand> maxGuestsCommand = SetEventMaxGuestsCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", 10);

        // Act
        if (maxGuestsCommand.IsFailure())
        {
            foreach (var error in maxGuestsCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(maxGuestsCommand.GetObj());

        if (result.IsFailure())
        {
            foreach (var error in result.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }

        // Assert
        Assert.False(result.IsFailure());
    }

    [Fact]
    public async Task GivenInvalidData_WhenSettingMaxGuests_ThenMaxGuestsNotSet()
    {
        // Arrange
        Result<SetEventMaxGuestsCommand> maxGuestsCommand = SetEventMaxGuestsCommand.Create("3b1d8789-e982-41b4-9f77-a7459fd6f51e", -1);

        // Act
        if (maxGuestsCommand.IsFailure())
        {
            foreach (var error in maxGuestsCommand.GetMessages()!)
                _testOutputHelper.WriteLine(error.GetMessage());
        }
        var result = await _handler.HandleAsync(maxGuestsCommand.GetObj());

        // Assert
        Assert.True(result.IsFailure());
    }
}