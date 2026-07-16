using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Domain.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {

        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<List<Order>> GetByUserAsync(string userId);

        Task<List<Order>> GetAllOrderAsync();

        Task<Order?> GetOrderDetailsAsync(Guid id);


    }
}
