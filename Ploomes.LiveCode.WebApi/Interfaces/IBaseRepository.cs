namespace Ploomes.LiveCode.WebApi.Interfaces;

public interface IBaseRepository<T, TKey>
    where T : new()
{
    Task<bool> CreateAsync(T entity);

    Task<bool> DeleteAsync(TKey id);

    Task<IEnumerable<T>> FetchAllAsync();

    Task<T> GetOneAsync(TKey id);
}
