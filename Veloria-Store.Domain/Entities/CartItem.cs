namespace Veloria_Store.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public Guid ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        // Navigation
        public Product Product { get; set; } = null!;
    }
}
