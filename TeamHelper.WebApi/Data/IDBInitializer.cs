
namespace TeamHelper.WebApi.Data
{
    public static class IDBInitializer
    {
        public static void EnsureDbCreated(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<TeamHelperIdentityDBContext>();
                    //context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    //context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole()
                    //{
                    //    Name = "Organization",
                    //    NormalizedName = "ORGANIZATION"
                    //});
                    //context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole()
                    //{
                    //    Name = "Athlete",
                    //    NormalizedName = "ATHLETE"
                    //});
                    //context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole()
                    //{
                    //    Name = "Device",
                    //    NormalizedName = "DEVICE"
                    //});
                    //context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole()
                    //{
                    //    Name = "Administrator",
                    //    NormalizedName = "ADMINISTRATOR"
                    //});
                    //context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole()
                    //{
                    //    Name = "Coach",
                    //    NormalizedName = "COACH"
                    //});
                    //context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
