using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Infrastructure.Data.Configuration
{
    public class BrandConfigure : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.HasMany(b => b.Products).WithOne(b => b.Brand).HasForeignKey(b => b.BrandId).OnDelete(DeleteBehavior.Restrict);
        }
    }

}
