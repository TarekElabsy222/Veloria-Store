namespace Veloria_Store.Application.ViewModels.CartItemViewModel
{
    public class CartItemVM
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}
