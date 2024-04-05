using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHelper.Domain;


namespace TeamHelper.Persistence.Configurations
{
    public class AthleteIndicatorsConfiguration : IEntityTypeConfiguration<AthleteIndicators>
    {
        public void Configure(EntityTypeBuilder<AthleteIndicators> builder)
        {
            builder.HasKey(node => node.Id);
        }
    }
}
