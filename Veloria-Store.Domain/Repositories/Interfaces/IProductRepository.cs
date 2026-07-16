using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetFeaturedAsync(int count);

        Task<List<Product>> GetPopularAsync(int count);

        Task<List<Product>> GetTrendyAsync(int count);

        Task<List<Product>> GetNewestAsync(int count);
        Task<List<Product>> GetTopSellingProductsAsync();
        Task<List<Product>> GetDealsOutletProductsAsync();
        Task<List<Product>> GetRelatedProductsAsync(Guid productId, Guid categoryId, int count);

        Task<List<Product>> GetPagedProductsAsync(int page, int pageSize);

        Task<List<Product>> SearchAsync(string keyword);

        Task<Product?> GetDetailsAsync(Guid id);
        Task<List<Product>> GetAllProductsAsync();
    }
}
