﻿using System.Diagnostics;
using ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.CommandDispatcher;

public class CommandDispatcherWithTimer(ICommandDispatcher next): ICommandDispatcher
{
    public async Task<Result<None>> DispatchAsync<TCommand>(TCommand? command)
    {
        var timer = Stopwatch.StartNew();
        var result = await next.DispatchAsync(command);
        timer.Stop();
        Console.WriteLine($"Command took {timer.ElapsedMilliseconds}ms to execute.");
        return result;
    }

    public T GetService<T>()
    {
        throw new NotImplementedException();
    }
}