using Veloria_Store.Domain.Enums;

namespace Veloria_Store.Application.ViewModels.Order
{
    public class OrderVM
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public decimal Total { get; set; }

        public OrderStatus Status { get; set; }
    }
}
