using Link.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Link.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.SurName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.FollowersCount)
                   .HasDefaultValue(0);

            builder.Property(u => u.FollowingCount)
                   .HasDefaultValue(0);

            builder.Property(u => u.PostCount)
                   .HasDefaultValue(0);

            builder.Property(u => u.View)
                   .HasDefaultValue(0);

            builder.Property(u => u.ImageUrl)
                   .HasMaxLength(255);

            builder.Property(u => u.About)
                   .HasMaxLength(255);


        }
    }
}
