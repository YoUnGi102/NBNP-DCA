using Domain.Common.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.UnitOfWork;

public class SqliteUnitOfWork(DbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}