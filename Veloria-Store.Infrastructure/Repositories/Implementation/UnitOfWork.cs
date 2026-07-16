using Microsoft.EntityFrameworkCore.Storage;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;
        #endregion

        #region Constructor
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Handle Functions
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion

    }
}
