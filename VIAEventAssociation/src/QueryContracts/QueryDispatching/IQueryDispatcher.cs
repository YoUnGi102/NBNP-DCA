using QueryContracts.Contract;

namespace QueryContracts.QueryDispatching;

public interface IQueryDispatcher
{
    public Task<TAnswer> DispatchAsync<TAnswer>(IQuery<TAnswer> query);
}