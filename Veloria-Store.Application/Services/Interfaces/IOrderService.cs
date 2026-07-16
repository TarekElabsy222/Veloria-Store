using Veloria_Store.Application.ViewModels.Order;

namespace Veloria_Store.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderAdminVM>> GetAllOrdersAsync();

        Task<OrderAdminDetailsVM?> GetByIdAsync(Guid id);

        Task UpdateStatusAsync(OrderStatusUpdateVM model);
        Task<List<UserOrderVM>> GetUserOrdersAsync(string userId);
    }
}
