
namespace Veloria_Store.Domain.Entities
{
    public class WishlistItem
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
