namespace QueryContracts.Contract;

public interface IQueryHandler<TQuery, TAnswer> where TQuery : IQuery<TAnswer>
{
    public Task<TAnswer> HandleAsync(TQuery query);
}