
using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glowal.Persistence.Configurations;

public class CareProductConfiguration : IEntityTypeConfiguration<CareProduct>
{
    public void Configure(EntityTypeBuilder<CareProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Brand)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Description)
               .IsRequired(false)
               .HasMaxLength(1000);

        builder.Property(x => x.Ingredients)
               .IsRequired(false)
               .HasMaxLength(2000);

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.HasOne(x => x.SkinType)
               .WithMany(st => st.CareProducts)
               .HasForeignKey(x => x.SkinTypeId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Reviews)
               .WithOne(r => r.Product)
               .HasForeignKey(r => r.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ProductProblems)
               .WithOne(pp => pp.CareProduct)
               .HasForeignKey(pp => pp.CareProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Articles)
               .WithOne(a => a.Product)
               .HasForeignKey(a => a.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

