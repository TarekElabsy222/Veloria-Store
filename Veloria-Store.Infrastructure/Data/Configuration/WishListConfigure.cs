using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Infrastructure.Data.Configuration
{
    public class WishListConfigure : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.ProductId)
                   .IsRequired();

            builder.HasOne(x => x.Product)
                    .WithMany(x => x.WishlistItems)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Prevent duplicate products in the same user's wishlist
            builder.HasIndex(x => new { x.UserId, x.ProductId })
                   .IsUnique();
        }
    }
}
