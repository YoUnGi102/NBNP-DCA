namespace Domain.Aggregates.Creator;

public interface ICreatorRepository
{
    public Task<Creator?> GetAsync(int id);

    public Task<Creator?> SaveAsync(Creator e);
}