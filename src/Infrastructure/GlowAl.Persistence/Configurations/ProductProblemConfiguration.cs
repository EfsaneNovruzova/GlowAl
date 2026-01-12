using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class ProductProblemConfiguration : IEntityTypeConfiguration<ProductProblem>
{
    public void Configure(EntityTypeBuilder<ProductProblem> builder)
    {
        builder.ToTable("ProductProblems");

        // Primary key artıq BaseEntity.Id-dən gəlir
        builder.HasKey(pp => pp.Id);

        // Relations
        builder.HasOne(pp => pp.CareProduct)
               .WithMany(p => p.ProductProblems)
               .HasForeignKey(pp => pp.CareProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pp => pp.SkinProblem)
               .WithMany(sp => sp.ProductProblems)
               .HasForeignKey(pp => pp.SkinProblemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}


