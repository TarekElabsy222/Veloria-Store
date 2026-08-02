using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;

namespace Veloria_Store.Infrastructure.Repositories.Implementation
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly AppDbContext _context;

        public ProductImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductImage image)
        {
            await _context.Images.AddAsync(image);
        }

        public async Task AddRangeAsync(IEnumerable<ProductImage> images)
        {
            await _context.Images.AddRangeAsync(images);
        }

        public void RemoveRange(IEnumerable<ProductImage> images)
        {
            _context.Images.RemoveRange(images);
        }
    }
}
