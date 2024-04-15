using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Common.UnitOfWork;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
}