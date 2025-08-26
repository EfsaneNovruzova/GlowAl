using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(a => a.Content)
               .IsRequired();
        builder.HasOne(a => a.SkinProblem)
               .WithMany()
               .HasForeignKey(a => a.SkinProblemId)
               .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(a => a.SkinType)
               .WithMany()
               .HasForeignKey(a => a.SkinTypeId)
               .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(a => a.Product)
               .WithMany(p => p.Articles)
               .HasForeignKey(a => a.ProductId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}

