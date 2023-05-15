using GSN.Application;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Repositories;
internal class Repository<T, TKey> : IRepository<T, TKey> where T : class
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public Repository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetAsync(TKey id)
    {
        using var context = _contextFactory.CreateDbContext();
        T? result = await context.Set<T>().FindAsync(id);
        if (result == null)
        {
            throw new ArgumentException($"Item with id{id} not found in database.");
        }
        return result;
    }

    public async Task<T> AddAsync(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<T>().FindAsync(id);
        if (entity == null)
            return false;

        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}
