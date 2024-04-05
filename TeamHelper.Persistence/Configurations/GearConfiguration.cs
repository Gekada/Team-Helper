using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamHelper.Domain;

namespace TeamHelper.Persistence.Configurations
{
    public class GearConfiguration : IEntityTypeConfiguration<Gear>
    {
        public void Configure(EntityTypeBuilder<Gear> builder)
        {
        }
    }
}
