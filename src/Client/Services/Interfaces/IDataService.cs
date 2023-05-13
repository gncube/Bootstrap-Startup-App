namespace Client.Services.Interfaces;

public interface IDataService<T, TKey>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(TKey key);
    Task Update(T t);
}
