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
    public class LinkeConfiguration : IEntityTypeConfiguration<Linke>
    {
        public void Configure(EntityTypeBuilder<Linke> builder)
        {
            builder.HasKey(l => l.LinkeID);

            builder.Property(l => l.LinkName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(l => l.LinkUrl)
                   .IsRequired()
                   .HasMaxLength(255);

        }
    }
}
