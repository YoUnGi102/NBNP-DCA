namespace Domain.Common.Repository;

public interface IGenericRepository<T>
{
    public Task<T> GetAsync(Guid id);

    public Task AddAsync(T e);

    public Task RemoveAsync(Guid id);

}