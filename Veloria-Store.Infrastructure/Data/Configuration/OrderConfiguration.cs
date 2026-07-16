using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderNumber)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.CustomerName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.Country)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.City)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Address)
                   .HasMaxLength(250)
                   .IsRequired();



            builder.Property(x => x.OrderNote)
                   .HasMaxLength(500);

            builder.Property(x => x.SubTotal)
                   .HasColumnType("decimal(18,2)");



            builder.Property(x => x.Total)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Status)
                   .HasConversion<int>();

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasMany(x => x.OrderItems)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
