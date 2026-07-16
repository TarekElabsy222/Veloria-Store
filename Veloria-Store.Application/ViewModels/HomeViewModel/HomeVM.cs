using Veloria_Store.Application.ViewModels.CategoryViiewModels;
using Veloria_Store.Application.ViewModels.ProductViewModels;

namespace Veloria_Store.Application.ViewModels.HomeViewModel
{
    public class HomeVM
    {
        public List<CategoryVM> Categories { get; set; } = new();

        public List<ProductCardVM> FeaturedProducts { get; set; } = [];
        public List<ProductShowCaseVM> TopSellingProducts { get; set; } = new();


        public List<ProductCardVM> PopularProducts { get; set; } = [];

        public List<ProductCardVM> NewProducts { get; set; } = [];

        public List<ProductShowCaseVM> TrendyProducts { get; set; } = [];
        public List<ProductShowCaseVM> DealsOutletProducts { get; set; } = [];
    }
}
