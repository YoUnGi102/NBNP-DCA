﻿using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace UnitTests.Fakes.Moks.Event;

public class UpdateEventTitleHandlerMock: ICommandHandler<UpdateEventTitleCommand>
{
    private bool _reachedHere = false;
    public async Task<Result<None>> HandleAsync(UpdateEventTitleCommand? command)
    {
        _reachedHere = true;
        return command != null ? ResultSuccess<None>.CreateEmptyResult() : ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
    }
    
    public bool ReachedHere() => _reachedHere;
}