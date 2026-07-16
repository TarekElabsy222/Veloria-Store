using Microsoft.EntityFrameworkCore;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class CartRepository : Repository<CartItem>, ICartRepository
    {
        public CartRepository(AppDbContext _context):base(_context) { }

        public async Task ClearAsync(string userId)
        {
            var items = await _context.CartItems
                .Where(x => x.UserId == userId)
                .ToListAsync();

            _context.CartItems.RemoveRange(items);
        }

        public async Task<List<CartItem>> GetAllAsync(string userId)
        {
            return await _context.CartItems
            .Include(x => x.Product).ThenInclude(x => x.Images)
            .Include(x => x.Product).ThenInclude(x => x.Category)
            .Where(x => x.UserId == userId)
            .ToListAsync();
        }

        public async Task<CartItem?> GetAsync(string userId, Guid productId)
        {
            return await _context.CartItems
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
        }

    }
}
