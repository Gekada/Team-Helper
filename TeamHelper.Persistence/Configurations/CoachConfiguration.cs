﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHelper.Domain;

namespace TeamHelper.Persistence.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasKey(node => node.Id);
            builder.HasIndex(node => node.Id);
            builder.Property(node => node.Name).IsRequired();
            builder.Property(node => node.Age).IsRequired().HasMaxLength(2);
            builder.Property(node => node.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(node => node.Email).IsRequired().HasMaxLength(50);

        }
    }
}
