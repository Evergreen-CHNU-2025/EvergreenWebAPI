namespace EvergreenWebAPI.Repositories.Abstractions;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    void UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    void DeleteRange(T[] entities);
}