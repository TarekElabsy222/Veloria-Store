using Veloria_Store.Domain.Entities;
using Veloria_Store.Domain.Enums;

namespace Veloria_Store.Application.ViewModels.Order
{
    public class OrderAdminVM
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public OrderStatus Status { get; set; }
        public string StatusName => Status.ToString();

        public DateTime CreatedAt { get; set; }
    }
}
