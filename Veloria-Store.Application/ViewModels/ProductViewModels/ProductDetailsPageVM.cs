namespace Veloria_Store.Application.ViewModels.ProductViewModels
{
    public class ProductDetailsPageVM
    {
        public ProductDetailsVM Product { get; set; } = new();

        public List<ProductCardVM> RelatedProducts { get; set; } = [];
    }
}
