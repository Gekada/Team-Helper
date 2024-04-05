using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHelper.Domain;

namespace TeamHelper.Persistence.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(node => node.Id);
            builder.HasIndex(node => node.Id);
            builder.Property(node => node.Name).IsRequired();
            builder.Property(node => node.Adress).IsRequired();
            builder.Property(node => node.PhoneNumber).IsRequired().HasMaxLength(15);
        }
    }
}
