using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using TeamHelper.Persistence.Configurations;

namespace TeamHelper.Persistence
{
    public class TeamHelperDBContext : DbContext, ITeamHelperDBContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<AthleteIndicators> AthleteIndicators { get; set; }
        public DbSet<IndicatorsData> IndicatorsDatas { get; set; }
        public DbSet<Gear> Gears { get; set; }

        public TeamHelperDBContext(DbContextOptions options) : base(options)
        {
        }
        //maybe delete later
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AthleteConfiguration());
            builder.ApplyConfiguration(new CoachConfiguration());
            builder.ApplyConfiguration(new OrganizationConfiguration());
            builder.ApplyConfiguration(new TrainingConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new AthleteIndicatorsConfiguration());
            base.OnModelCreating(builder);
        }
    }
}