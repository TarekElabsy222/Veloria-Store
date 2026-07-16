using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.Property(p => p.DiscountPercentage).HasColumnType("decimal(5,2)");

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(p => p.Images).WithOne(i => i.Product).HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }

}
