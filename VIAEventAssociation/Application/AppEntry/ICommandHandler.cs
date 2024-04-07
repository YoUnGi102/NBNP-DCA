using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry;

public interface ICommandHandler<in TCommand>
{
    Task<Result<None>> HandleAsync(TCommand? command);
}