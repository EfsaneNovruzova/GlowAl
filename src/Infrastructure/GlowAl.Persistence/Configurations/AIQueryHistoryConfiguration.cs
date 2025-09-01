namespace GlowAl.Persistence.Configurations;
using global::GlowAl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class AIQueryHistoryConfiguration : IEntityTypeConfiguration<AIQueryHistory>
    {
        public void Configure(EntityTypeBuilder<AIQueryHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Response)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(u => u.AIQueryHistories)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }


