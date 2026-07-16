using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Veloria_Store.Application.ViewModels.ProductViewModels
{
    public class ProductUpdateVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsPopular { get; set; }

        public bool IsTrendy { get; set; }

        public decimal DiscountPercentage { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        public List<IFormFile>? Images { get; set; }

        public List<string> ImageUrls { get; set; } = new();
    }
}
