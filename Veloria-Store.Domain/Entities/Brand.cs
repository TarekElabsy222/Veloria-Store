using System.ComponentModel.DataAnnotations;

namespace Veloria_Store.Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
