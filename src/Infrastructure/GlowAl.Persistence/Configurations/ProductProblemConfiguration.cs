using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations;

public class ProductProblemConfiguration : IEntityTypeConfiguration<ProductProblem>
{
    public void Configure(EntityTypeBuilder<ProductProblem> builder)
    {
        builder.HasKey(pp => pp.Id);

        builder.HasOne(pp => pp.Product)
               .WithMany(p => p.ProductProblems)
               .HasForeignKey(pp => pp.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pp => pp.Problem)
               .WithMany(p => p.ProductProblems)
               .HasForeignKey(pp => pp.ProblemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

