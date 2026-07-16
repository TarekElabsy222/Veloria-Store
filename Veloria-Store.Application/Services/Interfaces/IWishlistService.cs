using Veloria_Store.Application.ViewModels.ProductViewModels;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface IWishlistService
    {
        public Task AddAsync(string userId, Guid productId);

        public Task<List<ProductCardVM>> GetAsync(string userId);

        public Task RemoveAsync(string userId, Guid productId);
        Task<int> CountAsync(string userId);
    }
}
