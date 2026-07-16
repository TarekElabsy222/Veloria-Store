using Microsoft.EntityFrameworkCore;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class WishlistRepository : Repository<WishlistItem>, IWishlistRepository
    {
        public WishlistRepository(AppDbContext context) : base(context) { }


        public async Task<bool> ExistsAsync(string userId, Guid productId)
        {
            return await _context.WishlistItems.AnyAsync(x => x.UserId == userId && x.ProductId == productId);
        }

        public async Task RemoveAsync(string userId, Guid productId)
        {
            var item = await _context.WishlistItems .FirstOrDefaultAsync(x =>  x.UserId == userId && x.ProductId == productId);

            if (item != null)
            {
                _context.WishlistItems.Remove(item);
            }
        }

        public async Task<List<WishlistItem>> GetByUserAsync(string userId)
        {
            return await _context.WishlistItems.Include(x => x.Product).ThenInclude(x => x.Images)
                .Include(x => x.Product).ThenInclude(x => x.Category)
                .Where(x => x.UserId == userId) .ToListAsync();
        }

    }
}
