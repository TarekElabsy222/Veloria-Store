namespace Veloria_Store.Application.ViewModels.ProductViewModels
{
    public class ProductShowCaseVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
