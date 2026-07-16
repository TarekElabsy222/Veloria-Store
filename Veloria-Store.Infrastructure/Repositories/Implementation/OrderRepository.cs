using Microsoft.EntityFrameworkCore;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext _context) : base(_context) { }

        public async Task<List<Order>> GetByUserAsync(string userId)
        {
            return await _context.Orders

                .Include(x => x.OrderItems)

                .Where(x => x.UserId == userId)

                .OrderByDescending(x => x.CreatedAt)

                .ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<List<Order>> GetAllOrderAsync()
        {
            return await _context.Orders .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderDetailsAsync(Guid id)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
