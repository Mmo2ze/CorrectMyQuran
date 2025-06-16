using System.Linq.Expressions;
using CorectMyQuran.DateBase.Common.Models;

namespace CorectMyQuran.DateBase.Common.Repositories;

public interface IRepository<TEntity, TId> where TEntity : Aggregate<TId> where TId : class 
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    void Add(TEntity entity);
    void Update (TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task<List<TEntity>> GetAllAsync();
    IQueryable<TEntity> GetQueryable();
    bool Any(Expression<Func<TEntity, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity?> FindAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<List<TEntity>> Where (Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> WhereQueryable(Expression<Func<TEntity, bool>> predicate);
}