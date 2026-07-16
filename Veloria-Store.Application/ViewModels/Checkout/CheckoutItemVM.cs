namespace Veloria_Store.Application.ViewModels.Checkout
{
    public class CheckoutItemVM
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductImage { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
        public decimal Shipping { get; set; }
    }
}
