using System.Diagnostics;
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
        string[] timer_message = { $"Command took {timer.ElapsedMilliseconds}ms to execute." };
        Message[] messages = ResultHelper<None>.CombineResultMessages([result, ResultFailure<None>
            .CreateMessageResult(default,timer_message)]);;
        if (result is ResultFailure<None>)
            return ResultFailure<None>.CreateMessageResult(new None(), messages);
        return ResultSuccess<None>.CreateMessageResult(new None(), messages);
    }

    public T GetService<T>()
    {
        throw new NotImplementedException();
    }
}