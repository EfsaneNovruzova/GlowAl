using GlowAl.Domain.Entities;
using GlowAl.Domain.Enums.OrderEnum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId)
                   .IsRequired();

            builder.Property(o => o.TotalAmount)
                  .HasColumnType("decimal(18,2)")
                      .IsRequired();


            builder.Property(o => o.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(o => o.IsPaid)
                   .HasDefaultValue(false);

            builder.Property(o => o.Status)
                   .HasDefaultValue(OrderStatus.Pending);

            // Relationship: Order -> OrderItems
            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
