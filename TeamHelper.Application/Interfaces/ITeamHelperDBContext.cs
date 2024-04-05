using Microsoft.EntityFrameworkCore;
using TeamHelper.Domain;
namespace TeamHelper.Application.Interfaces
{
    using AthleteIndicators = TeamHelper.Domain.AthleteIndicators;
    using IndicatorsData = TeamHelper.Domain.IndicatorsData;
    public interface ITeamHelperDBContext
    {
        DbSet<Organization> Organizations { get; set; }
        DbSet<Athlete> Athletes { get; set; }
        DbSet<Coach> Coaches { get; set; }
        DbSet<Training> Trainings { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<AthleteIndicators> AthleteIndicators { get; set; }
        DbSet<IndicatorsData> IndicatorsDatas { get; set; }
        DbSet<Gear> Gears { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
