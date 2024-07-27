using Link.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Persistence.Configurations
{
    public class ProfileCommentConfiguration : IEntityTypeConfiguration<ProfileComment>
    {
        public void Configure(EntityTypeBuilder<ProfileComment> builder)
        {
            builder.HasKey(pc => pc.ProfileCommentID);

            builder.Property(pc => pc.Comment)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(pc => pc.View)
                   .IsRequired();

            builder.Property(pc => pc.Like)
                   .IsRequired();

            builder.Property(pc => pc.WriterID)
                   .IsRequired();

            builder.Property(pc => pc.Hidden)
                   .IsRequired();

            builder.Property(pc => pc.Time)
                   .IsRequired();


            builder.HasOne(pc => pc.Answers)
                   .WithOne(a => a.ProfileComment)
                   .HasForeignKey<Answer>(a => a.ProfileCommentID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
