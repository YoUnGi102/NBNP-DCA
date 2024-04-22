using Domain.Aggregates.Events;
using Domain.Aggregates.Guests;
using Domain.Common.UnitOfWork;
using UnitTests.Fakes;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.Features.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;
using Xunit;
using Xunit.Abstractions;

namespace VIAEventAssociation.Tests.UnitTests.Features.Guest.RegisterAccount;

public class RegisterAccountHandlerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ICommandHandler<RegisterAccountCommand> _handler;

    private IGuestRepository _repository;
    private IUnitOfWork _uow;

    public RegisterAccountHandlerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repository = new GuestRepoFake();
        _uow = new UnitOfWorkFake();
        _handler = new RegisterAccountHandler(_repository, _uow);
    }

    [Fact]
    public async Task GivenValidData_WhenRegisteringAccount_ThenAccountRegistered()
    {
        // Arrange
        Result<RegisterAccountCommand> cmd = RegisterAccountCommand.Create("guest1@example.com", "Joe", "Shmoe");

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
    public async Task GivenInvalidData_WhenRegisteringAccount_ThenAccountNotRegistered()
    {
        // Arrange
        Result<RegisterAccountCommand> cmd = RegisterAccountCommand.Create("guest@gmail.com", "Joe", "Shmoe");

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