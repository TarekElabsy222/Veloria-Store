using Microsoft.EntityFrameworkCore;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {

        public BrandRepository(AppDbContext _context)  : base(_context)
        {           
        }

        public async Task<List<Brand>> GetWithProductsAsync()
        {
            return await _context.Brands.Include(b => b.Products).ToListAsync();
        }

        public async Task<Brand?> GetDetailsAsync(Guid id)
        {
            return await _context.Brands.Include(b => b.Products)
                    .ThenInclude(p => p.Images).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }
    }
}
