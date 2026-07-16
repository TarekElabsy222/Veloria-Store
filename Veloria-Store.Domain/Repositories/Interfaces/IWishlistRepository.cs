using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface IWishlistRepository:IRepository<WishlistItem>
    {
        Task<bool> ExistsAsync(string userId, Guid productId);

        Task RemoveAsync(string userId, Guid productId);

        Task<List<WishlistItem>> GetByUserAsync(string userId);

    }
}
