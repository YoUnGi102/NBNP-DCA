namespace Domain.Common.Repository;

public interface IGenericRepository<T>
{
    public Task<T> GetAsync(int id);

    public Task AddAsync(T e);

    public Task RemoveAsync(int id);

}