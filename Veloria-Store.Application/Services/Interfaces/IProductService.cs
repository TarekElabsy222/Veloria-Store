using Veloria_Store.Application.ViewModels.HomeViewModel;
using Veloria_Store.Application.ViewModels.ProductViewModels;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductCardVM>> GetFeaturedAsync(int count);

        Task<List<ProductCardVM>> GetPopularAsync(int count);

        Task<List<ProductCardVM>> GetNewestAsync(int count);

        Task<List<ProductCardVM>> GetTrendyAsync(int count);
        Task<List<ProductShowCaseVM>> GetTopSellingAsync();

        Task<List<ProductShowCaseVM>> GetDealsOutletAsync();

        Task<ProductDetailsPageVM?> GetDetailsAsync(Guid id);

        Task<List<ProductCardVM>> GetPagedProductsAsync(int page, int pageSize);

        Task<List<ProductCardVM>> SearchAsync(string keyword);
        Task<ShopVM> GetShopProductsAsync(int page);

        Task<HomeVM> GetHomeAsync();

        // admin services
        Task<IEnumerable<ProductVM>> GetAllProductsAsync();

        Task<ProductVM?> GetByIdAsync(Guid id);

        Task CreateAsync(ProductCreateVM model);

        Task UpdateAsync(ProductUpdateVM model);

        Task DeleteAsync(Guid id);

    }
}
