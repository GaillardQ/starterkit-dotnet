using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateDotNet.Domains;
using TemplateDotNet.Utils;

namespace TemplateDotNet.Repositories;

public abstract class CoreRepository<T, U> : ICoreRepository<T> where T : DataCoreEntity where U : DbContext
{
    protected readonly U _context;
    protected readonly DbSet<T> _set;
    protected CoreRepository(U context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    /// <summary>
    /// Crée une nouvelle entité
    /// </summary>
    /// <param name="entity">Entité à créer</param>
    /// <returns>Entité créée et enrichie par la base de données</returns>
    public Task<T> Create(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), ErrorMessages.ErrorRequiredParam);

        if (entity is DataCoreEntity && entity!.Id == Guid.Empty)
        {
            (entity as DataCoreEntity)!.Id = Guid.NewGuid();
        }

        var result = _set.Add(entity);
        _context.SaveChanges();
        return Task.FromResult(result.Entity);
    }

    /// <summary>
    /// Crée une nouvelle entité de façon asynchrone
    /// </summary>
    /// <param name="entity">Entité à créer</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Entité créée et enrichie par la base de données</returns>
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), ErrorMessages.ErrorRequiredParam);

        if (entity is DataCoreEntity && entity!.Id == Guid.Empty)
        {
            (entity as DataCoreEntity)!.Id = Guid.NewGuid();
        }

        var result = await _set.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Crée une nouvelle entité
    /// </summary>
    /// <param name="entity">Entité à créer</param>
    /// <returns>Entité créée et enrichie par la base de données</returns>
    public Task<List<T>> CreateRange(List<T> entities)
    {
        if (entities == null || entities.Count == 0)
            throw new ArgumentNullException(nameof(entities), ErrorMessages.ErrorRequiredParam);

        List<T> result = new();

        entities.ForEach(entity =>
        {
            if (entity is DataCoreEntity && entity!.Id == Guid.Empty)
            {
                (entity as DataCoreEntity)!.Id = Guid.NewGuid();
            }

            result.Add(_set.Add(entity).Entity);

        });
        _context.SaveChanges();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Modifie une entité existante
    /// </summary>
    /// <param name="entity">Entité à modifier</param>
    /// <returns>Entité modifiée et enrichie par la base de données</returns>
    public Task<T> Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), ErrorMessages.ErrorRequiredParam);

        var result = _set.Update(entity);
        _context.SaveChanges();
        return Task.FromResult(result.Entity);
    }

    /// <summary>
    /// Supprime une entité
    /// </summary>
    /// <param name="entity">Entité à supprimer</param>
    /// <returns></returns>
    public Task Delete(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), ErrorMessages.ErrorRequiredParam);

        _set.Remove(entity);
        _context.SaveChanges();

        return Task.FromResult(0);
    }

    /// <summary>
    /// Recherche une entité
    /// </summary>
    /// <param name="keys">Données de la clé primaire</param>
    /// <returns>Entité trouvée</returns>
    public async Task<T?> Find(CancellationToken cancellationToken, params object[] keys)
    {
        if (keys == null || keys.Length == 0)
            throw new ArgumentNullException(nameof(keys), ErrorMessages.ErrorRequiredParam);

        return await _set.FindAsync(keys, cancellationToken);
    }

    /// <summary>
    /// Permet de rechercher toutes les entités
    /// </summary>
    /// <returns></returns>
    public async Task<List<T>> FindAll(CancellationToken cancellationToken)
        => await _context.Set<T>().AsQueryable().ToListAsync(cancellationToken);
 

    /// <summary>
    /// Permet de rechercher toutes les entités selon le prédicat donnée
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    protected async Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        => await _context.Set<T>().AsQueryable().Where(predicate).ToListAsync(cancellationToken);
}
