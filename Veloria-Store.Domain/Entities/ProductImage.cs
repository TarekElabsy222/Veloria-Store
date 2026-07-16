using System.ComponentModel.DataAnnotations;

namespace Veloria_Store.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        // Navigation Property
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
