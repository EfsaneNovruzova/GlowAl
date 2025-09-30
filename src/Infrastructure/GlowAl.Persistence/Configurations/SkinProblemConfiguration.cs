using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations
{
    public class SkinProblemConfiguration : IEntityTypeConfiguration<SkinProblem>
    {
        public void Configure(EntityTypeBuilder<SkinProblem> builder)
        {
            builder.ToTable("SkinProblems");

            // Primary key
            builder.HasKey(sp => sp.Id);

            // Properties
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

            // Relation with ProductProblem
            builder.HasMany(sp => sp.ProductProblems)
                   .WithOne(pp => pp.SkinProblem)
                   .HasForeignKey(pp => pp.SkinProblemId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relation with Article (optional)
            builder.HasMany(sp => sp.Articles)
                   .WithOne(a => a.SkinProblem)
                   .HasForeignKey(a => a.SkinProblemId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            builder.HasData(
                new SkinProblem
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Yağlı dəri",
                    Description = "Yağ balansını tənzimləmək üçün məhsullar"
                },
                new SkinProblem
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Quru dəri",
                    Description = "Nəmləndirici məhsullar"
                },
                new SkinProblem
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Akne",
                    Description = "Akne qarşısı üçün məhsullar"
                }
            );
        }
    }
}
