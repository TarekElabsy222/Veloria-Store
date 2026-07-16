using System.ComponentModel.DataAnnotations;

namespace Veloria_Store.Application.ViewModels.Checkout
{
    public class CheckoutVM
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;


        public string? OrderNote { get; set; }

        public List<CheckoutItemVM> Items { get; set; } = new();

        public decimal SubTotal { get; set; }

        public decimal Shipping { get; set; }

        public decimal Total { get; set; }
    }
}
