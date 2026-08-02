using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface IProductImageRepository
    {
        Task AddAsync(ProductImage image);
        Task AddRangeAsync(IEnumerable<ProductImage> images);
        void RemoveRange(IEnumerable<ProductImage> images);
    }
}
