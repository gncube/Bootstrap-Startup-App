using GSN.Application;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Repositories;
internal class Repository<T, TKey> : IRepository<T, TKey> where T : class
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreatedAsync();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetAsync(TKey id)
    {
        T? result = await _context.Set<T>().FindAsync(id);
        if (result == null)
        {
            throw new ArgumentException($"Item with id{id} not found in database.");
        }
        return result;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
            return false;

        _context.Set<T>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
