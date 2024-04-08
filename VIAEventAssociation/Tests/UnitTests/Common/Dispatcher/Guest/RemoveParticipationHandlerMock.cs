using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Guest;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace UnitTests.Common.Dispatcher.Guest;

public class RemoveParticipationHandlerMock: ICommandHandler<RemoveParticipationCommand>
{
    private bool _reachedHere = false;
    public async Task<Result<None>> HandleAsync(RemoveParticipationCommand? command)
    {
        _reachedHere = true;
        return command != null ? ResultSuccess<None>.CreateEmptyResult() : ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
    }
    
    public bool ReachedHere() => _reachedHere;
}