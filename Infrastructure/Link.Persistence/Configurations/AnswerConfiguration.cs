using Link.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Link.Persistence.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.AnswerID);

            builder.Property(a => a.AnswerText)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(a => a.View)
                   .HasMaxLength(10);

            builder.Property(a => a.LikeCount)
                   .HasMaxLength(10);



        }
    }
}
