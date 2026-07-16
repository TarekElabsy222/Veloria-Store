using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<CartItem>
    {
        Task<List<CartItem>> GetAllAsync(string userId);

        Task<CartItem?> GetAsync(string userId, Guid productId);
        Task ClearAsync(string userId);

    }
}
