using System.Linq.Expressions;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[]? Includes);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[]? Includes);
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void update(TEntity entity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
    }
}
