using Veloria_Store.Application.ViewModels.Checkout;
using Veloria_Store.Domain.Enums;

namespace Veloria_Store.Application.ViewModels.Order
{
    public class OrderDetailsVM
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;


        public string? OrderNote { get; set; }

        public decimal SubTotal { get; set; }


        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; }

        public List<CheckoutItemVM> Items { get; set; } = new();
    }
}
