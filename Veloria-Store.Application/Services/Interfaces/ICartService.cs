using Veloria_Store.Application.ViewModels.CartItemViewModel;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task AddAsync(Guid productId);

        Task RemoveAsync(Guid productId);

        Task IncreaseAsync(Guid productId);

        Task DecreaseAsync(Guid productId);
        Task MergeCartAsync();

        Task<CartVM> GetAsync();

        Task<int> CountAsync();
    }
}
