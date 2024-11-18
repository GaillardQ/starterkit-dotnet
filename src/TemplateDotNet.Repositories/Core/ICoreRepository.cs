using TemplateDotNet.Domains;

namespace TemplateDotNet.Repositories;

public interface ICoreRepository<T> where T : DataCoreEntity?
{
    Task<T> Create(T entity);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<List<T>> CreateRange(List<T> entities);
    Task Delete(T entity);
    Task<T?> Find(CancellationToken cancellationToken, params object[] keys);
    Task<List<T>> FindAll(CancellationToken cancellationToken);
    Task<T> Update(T entity);
}