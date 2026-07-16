using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veloria_Store.Application.ViewModels.ProductViewModels
{
    public class ProductVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public Guid BrandId { get; set; }

        public string BrandName { get; set; } = string.Empty;

        public decimal DiscountPercentage { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsPopular { get; set; }

        public bool IsTrendy { get; set; }

        public List<string> Images { get; set; } = new();
    }

}
