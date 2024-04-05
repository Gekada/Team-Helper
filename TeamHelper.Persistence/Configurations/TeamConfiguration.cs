using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHelper.Domain;


namespace TeamHelper.Persistence.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(node => node.Id);
            builder.HasIndex(node => node.Id);
            builder.Property(node => node.Name).IsRequired().HasMaxLength(20);
            builder.Property(node => node.MembNumber).IsRequired().HasMaxLength(2);
        }
    }
}
