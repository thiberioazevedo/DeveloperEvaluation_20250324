using DeveloperEvaluation.Domain.Common;
using DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IRepository using Entity Framework Core
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DefaultContext _context;
    internal DbSet<T> DbSet {
        get {
            return _context.Set<T>();
        }
    }

    /// <summary>
    /// Initializes a new instance of Repository
    /// </summary>
    /// <param name="context">The database context</param>
    public Repository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new entity in the database
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created entity</returns>
    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.Number = GetLastNumber() + 1;
        await DbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Retrieves a entity by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetQuery().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<T?> GetAllAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a entity from the database
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        DbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public virtual IQueryable<T> GetQuery()
    {
        return DbSet.AsNoTracking();
    }
    public virtual IQueryable<T> GetQuery(string? searchText, string? colunaOrdenacao, bool ordenacaoAscendente, CancellationToken cancellationToken)
    {
        return GetQuery();
    }
    public async Task<PaginatedList<T>> GetPaginatedList(string? searchText, string? columnOrder, bool asc, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var queryable = GetQuery(searchText, columnOrder, asc, cancellationToken);

        var pagination = await PaginatedList<T>.CreateInstanceAsync(pageNumber, pageSize, queryable);

        return await Task.FromResult(pagination);
    }
    /*internal static IQueryable<T> OrderBy<TKey>(IQueryable<T> query, bool ordenacaoAscendente, Expression<Func<T, TKey>> expression)
    {
        return ordenacaoAscendente ? query.OrderBy(expression) : query.OrderByDescending(expression);
    }*/

    public int GetLastNumber()
    {
        var model = DbSet.OrderByDescending(s => s.Number).FirstOrDefault();
        return model?.Number ?? 0;
   }
}