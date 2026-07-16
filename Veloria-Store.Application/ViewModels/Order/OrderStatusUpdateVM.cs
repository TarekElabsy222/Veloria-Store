using Veloria_Store.Domain.Enums;

namespace Veloria_Store.Application.ViewModels.Order
{
    public class OrderStatusUpdateVM
    {
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }
    }
}
