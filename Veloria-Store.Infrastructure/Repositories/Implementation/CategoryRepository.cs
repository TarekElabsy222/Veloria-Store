using Microsoft.EntityFrameworkCore;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(AppDbContext _context) : base(_context)
        {
        }

        public async Task<List<Category>> GetWithProductsAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetDetailsAsync(Guid id)
        {
            return await _context.Categories.Include(c => c.Products)
                    .ThenInclude(p => p.Images).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
