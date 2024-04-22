using QueryContracts.Contract;

namespace QueryContracts.QueryDispatching;

public class QueryDispatcher(IServiceProvider serviceProvider): IQueryDispatcher
{
    public Task<TAnswer> DispatchAsync<TAnswer>(IQuery<TAnswer> query)
    {
        Type queryInterfaceWithTypes = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TAnswer));
        dynamic handler = serviceProvider.GetService(queryInterfaceWithTypes)!;

        if (handler == null)
        {
            throw new InvalidOperationException((query.GetType(),typeof(TAnswer)).ToString());
        }
        return handler.HandleAsync((dynamic)query);
    }
}