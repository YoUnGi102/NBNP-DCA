using Domain.Common.UnitOfWork;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace UnitTests.Fakes;

public class UnitOfWorkFake : IUnitOfWork
{
    public Task<Result<None>> SaveChangesAsync()
    {
        return Task.FromResult(ResultSuccess<None>.CreateEmptyResult());
    }
}