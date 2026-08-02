using Veloria_Store.Application.ViewModels.CategoryViiewModels;
using Veloria_Store.Application.ViewModels.ProductViewModels;

namespace Veloria_Store.Application.ViewModels.HomeViewModel
{
    public class ShopVM
    {
        public List<ProductCardVM> Products { get; set; } = [];

        public List<CategoryShopVM> Categories { get; set; } = [];

        public Guid? SelectedCategoryId { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalProducts { get; set; }

        public int PageSize { get; set; }
    }
}
