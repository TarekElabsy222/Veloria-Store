using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<int> GetProductsCountAsync();

        Task<int> GetOrdersCountAsync();

        Task<int> GetUsersCountAsync();

        Task<decimal> GetRevenueAsync();

        Task<List<Order>> GetRecentOrdersAsync(int count);
    }
}
