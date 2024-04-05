using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamHelper.Application.Interfaces;

namespace TeamHelper.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ITeamHelperDBContext, TeamHelperDBContext>(opts =>
            {
                opts.UseSqlServer(configuration["DbConnection"], b =>
                {
                    b.MigrationsAssembly("TeamHelper.WebApi");
                });
            });
            return services;
        }
    }
}
