using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                  .HasColumnType("decimal(18,2)")
                      .IsRequired();


            // Relationship: OrderItem -> Product
            builder.HasOne(oi => oi.Product)
                   .WithMany()
                   .HasForeignKey(oi => oi.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
