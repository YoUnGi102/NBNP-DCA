using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Dispatcher;

internal class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task<Result<None>> DispatchAsync<TCommand>(TCommand command) =>
        await GetService<ICommandHandler<TCommand>>().HandleAsync(command);

    public T GetService<T>() => (T)serviceProvider.GetService(typeof(T))! ?? throw new InvalidOperationException(nameof(T));
}