using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace UnitTests.Fakes.Moks.Event;

public class CreateEventHandlerMock : ICommandHandler<CreateEventCommand>
{
    private bool _reachedHere = false;
    public async Task<Result<None>> HandleAsync(CreateEventCommand command)
    {
        _reachedHere = true;
        return ResultSuccess<None>.CreateEmptyResult();
    }
    
    public bool ReachedHere() => _reachedHere;
}