namespace Veloria_Store.Application.ViewModels.CartItemViewModel
{
    public class CartVM
    {
        public List<CartItemVM> Items { get; set; } = [];

        public decimal SubTotal => Items.Sum(x => x.Total);

        public decimal Shipping => 0;

        public decimal Total => SubTotal + Shipping;
    }
}
