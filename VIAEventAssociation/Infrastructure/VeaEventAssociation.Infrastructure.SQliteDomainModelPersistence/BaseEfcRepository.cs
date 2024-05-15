using Domain.Common.Repository;
using Microsoft.EntityFrameworkCore;

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence;

public class BaseEfcRepository<T>(DmContext context) : IGenericRepository<T> where T : class
{
    
    public virtual async Task<T> GetAsync(Guid id)
    {
        var t = await context.Set<T>().FindAsync(id);
        if (t == null)
            throw new Exception("Entity not found");
        return t;
    }

    public virtual async Task AddAsync(T e) => await context.Set<T>().AddAsync(e);

    public virtual async Task RemoveAsync(Guid id)
    {
        var t = await context.Set<T>().FindAsync(id);
        if (t == null)
            throw new Exception("Entity not found");
        context.Set<T>().Remove(t);
    }
}