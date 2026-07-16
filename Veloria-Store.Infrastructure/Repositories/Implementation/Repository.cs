using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _entities;
        #endregion

        #region Constructor
        public Repository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        #endregion

        #region Handle Functions        
        public async Task AddAsync(TEntity entity)
        => await _entities.AddAsync(entity);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter == null)
                return await _entities.CountAsync();

            return await _entities.CountAsync(filter);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        => await _entities.AnyAsync(filter);


        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[]? Includes)
        {
            IQueryable<TEntity> query = _entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().ToListAsync();

        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        => await _entities.FindAsync(id);

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[]? Includes)
        {
            IQueryable<TEntity> query = _entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public void update(TEntity entity)
        {
            _entities.Update(entity);
        }
        #endregion
    }
}
