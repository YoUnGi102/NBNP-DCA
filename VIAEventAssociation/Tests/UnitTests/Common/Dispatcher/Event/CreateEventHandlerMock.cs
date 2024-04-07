using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace UnitTests.Fakes.Moks.Event;

public class CreateEventHandlerMock : ICommandHandler<CreateEventCommand>
{
    private bool _reachedHere = false;
    public async Task<Result<None>> HandleAsync(CreateEventCommand? command)
    {
        if (command != null)
        {
            _reachedHere = true;
            return ResultSuccess<None>.CreateEmptyResult();
        }

        return ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
    }
    
    public bool ReachedHere() => _reachedHere;
}