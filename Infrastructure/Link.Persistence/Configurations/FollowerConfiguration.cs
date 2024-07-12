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
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(f => f.FollowerID);

            builder.Property(f => f.UserName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(255);


        }
    }
}
