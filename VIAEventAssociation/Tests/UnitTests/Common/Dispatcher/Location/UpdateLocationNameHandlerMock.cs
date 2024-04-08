﻿using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Features;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace UnitTests.Common.Dispatcher.Location;

public class UpdateLocationNameHandlerMock : ICommandHandler<UpdateLocationNameCommand>
{
    private bool _reachedHere = false;

    public async Task<Result<None>> HandleAsync(UpdateLocationNameCommand? command)
    {
        _reachedHere = true;
        return command != null ? ResultSuccess<None>.CreateEmptyResult() : ResultFailure<None>.CreateMessageResult(new None(), ["Command is null."]);
    }

    public bool ReachedHere() => _reachedHere;
}