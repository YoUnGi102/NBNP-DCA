using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;

public interface ICommandDispatcher
{ 
    Task<Result<None>> DispatchAsync<TCommand>(TCommand? command);
    T GetService<T>();
}