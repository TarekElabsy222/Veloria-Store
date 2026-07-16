namespace Veloria_Store.Application.ViewModels.ProductViewModels
{
    public class ProductDetailsVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public decimal DiscountPercentage { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public List<string> Images { get; set; } = [];
    }

}
