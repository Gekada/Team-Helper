using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHelper.Domain;

namespace TeamHelper.Persistence.Configurations
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(node => node.Id);
            builder.HasIndex(node => node.Id);
            builder.Property(node => node.Date).IsRequired();
            builder.Property(node => node.Duration).IsRequired();
            builder.Property(node => node.Location).IsRequired();
        }
    }
}
