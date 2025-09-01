namespace GlowAl.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using global::GlowAl.Domain.Entities;



public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
           
            builder.Property(u => u.FulName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.RefreshToken)
                   .IsRequired(false);

            builder.Property(u => u.ExpiryDate)
                   .IsRequired();
            builder.HasOne(u => u.SkinType)
                   .WithMany()
                   .HasForeignKey(u => u.SkinTypeId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.AIQueryHistories)
                   .WithOne(q => q.User)
                   .HasForeignKey(q => q.UserId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(u => u.Reviews)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }


