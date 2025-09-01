using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        // Primary Key
        builder.HasKey(r => r.Id);

        // Properties
        builder.Property(r => r.Rating)
               .IsRequired();

        builder.Property(r => r.Comment)
               .IsRequired()
               .HasMaxLength(1000);

        // Relationship with Product
        builder.HasOne(r => r.Product)
               .WithMany(p => p.Reviews)
               .HasForeignKey(r => r.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        // Relationship with User
        builder.HasOne(r => r.User)
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

