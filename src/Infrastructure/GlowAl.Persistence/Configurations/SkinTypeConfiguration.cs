using GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlowAl.Persistence.Configurations
{
    public class SkinTypeConfiguration : IEntityTypeConfiguration<SkinType>
    {
        public void Configure(EntityTypeBuilder<SkinType> builder)
        {
            builder.ToTable("SkinTypes");

            // Primary Key
            builder.HasKey(st => st.Id);

            // Properties
            builder.Property(st => st.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(st => st.Description)
                   .IsRequired(false)
                   .HasMaxLength(1000);

            // Relation with CareProduct (One-to-Many)
            builder.HasMany(st => st.CareProducts)
                   .WithOne(cp => cp.SkinType)
                   .HasForeignKey(cp => cp.SkinTypeId)
                   .OnDelete(DeleteBehavior.SetNull); // Əgər SkinType silinsə, CareProduct-un SkinTypeId null olur
        }
    }
}
