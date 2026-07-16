using Microsoft.EntityFrameworkCore;
using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Product?> GetDetailsAsync(Guid id)
        {
            return await _context.Products.Include(p => p.Brand)
                .Include(p => p.Category).Include(p => p.Images)
                .AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Images).AsNoTracking().ToListAsync();
        }


        public async Task<List<Product>> GetFeaturedAsync(int count)
        {
            return await _context.Products.Where(p => p.IsFeatured)
                .Include(p => p.Images).Include(p => p.Category)
                .Take(count).AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> GetPopularAsync(int count)
        {
            return await _context.Products.Where(p => p.IsPopular)
                .Include(p => p.Images).Include(p => p.Category)
                .Take(count).AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> GetTrendyAsync(int count)
        {
            return await _context.Products.Where(p => p.IsTrendy)
                .Include(p => p.Images)
                .Take(count).AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> GetNewestAsync(int count)
        {
            return await _context.Products.Include(p => p.Images)
                .OrderByDescending(p => p.CreatedAt).Include(p => p.Category)
                .Take(count).AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> GetRelatedProductsAsync(Guid productId, Guid categoryId, int count)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId && p.Id != productId)
                .Include(p => p.Images).Include(p => p.Category)
                .Take(count).AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> GetPagedProductsAsync(int page, int pageSize)
        {
            return await _context.Products.Include(p => p.Images)
                .OrderByDescending(p => p.CreatedAt).Skip((page - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> SearchAsync(string keyword)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(keyword))
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Product>> GetTopSellingProductsAsync()
        {
            return await _context.Products.Include(p => p.Images)
                .OrderByDescending(p => p.SoldCount)
                .Take(3).AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> GetDealsOutletProductsAsync()
        {
            return await _context.Products.Where(p => p.DiscountPercentage > 0)
                .Include(p => p.Images).OrderByDescending(p => p.DiscountPercentage)
                .Take(3).AsNoTracking().ToListAsync();
        }
    }
}
