using System.ComponentModel.DataAnnotations;

namespace Veloria_Store.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsPopular { get; set; }

        public bool IsTrendy { get; set; }

        public int SoldCount { get; set; }

        public decimal DiscountPercentage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; }
    = new List<OrderItem>();
    }
}
