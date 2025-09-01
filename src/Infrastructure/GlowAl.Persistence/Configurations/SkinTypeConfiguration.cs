using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations
{
    public class SkinProblemConfiguration : IEntityTypeConfiguration<SkinProblem>
    {
        public void Configure(EntityTypeBuilder<SkinProblem> builder)
        {
            builder.HasKey(sp => sp.Id);
            builder.Property(sp => sp.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(sp => sp.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(sp => sp.Severity)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasDefaultValue("Medium");
            builder.HasMany(sp => sp.ProductProblems)
                   .WithOne(pp => pp.Problem)
                   .HasForeignKey(pp => pp.ProblemId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(sp => sp.Articles)
                   .WithOne(a => a.SkinProblem)
                   .HasForeignKey(a => a.SkinProblemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
