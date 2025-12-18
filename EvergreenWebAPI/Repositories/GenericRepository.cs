using EvergreenWebAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EvergreenWebAPI.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext dbContext;
    private readonly DbSet<T> table;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
        table = dbContext.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await table.AddAsync(entity);

        return entity;
    }

    public async Task<List<T>> GetAllAsync()
        => await table.ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id)
        => await table.FindAsync(id);

    public void UpdateAsync(T entity)
        => dbContext.Update(entity);

    public async Task DeleteAsync(Guid id)
    {
        var entity = await table.FindAsync(id);

        if (entity == null)
            return;

        table.Remove(entity);
    }

    public void DeleteRange(T[] entities)
        => table.RemoveRange(entities);

}