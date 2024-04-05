using TeamHelper.Persistence;
using TeamHelper.Application.Interfaces;
using TeamHelper.Application;
using TeamHelper.Application.Common.Mapping;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TeamHelper.WebApi.Data;
using Microsoft.AspNetCore.Identity;
using TeamHelper.WebApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using IdentityServer4.AccessTokenValidation;

namespace TeamHelper.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(ITeamHelperDBContext).Assembly));
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireDigit = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TeamHelperIdentityDBContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(opts =>
            {
                opts.CookieManager = new ChunkingCookieManager();
                opts.Cookie.HttpOnly = true;
                opts.Cookie.SameSite = SameSiteMode.Unspecified;
                opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opts.Cookie.Name = "TeamHelper.Identity.Cookie";
                opts.LoginPath = "/Auth/Login";
                opts.LogoutPath = "/Auth/Logout";
            });

            builder.Services.AddIdentityServer()
            .AddAspNetIdentity<AppUser>()
            .AddInMemoryApiResources(Configuration.ApiResources)
            .AddInMemoryPersistedGrants()
            .AddInMemoryIdentityResources(Configuration.IdentityResources)
            .AddInMemoryApiScopes(Configuration.ApiScopes)
            .AddInMemoryClients(Configuration.Clients)
            .AddDeveloperSigningCredential();

            builder.Services.AddDbContext<TeamHelperIdentityDBContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration["IDbConnection"]);
            });
            builder.Services.AddApplication();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = builder.Configuration["identity"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "TeamHelperWebApi";

                });
            //    .AddJwtBearer("Bearer", opts =>
            //{
            //    opts.Authority = "https://localhost:5001";
            //    opts.Audience = "TeamHelperWebApi";
            //    opts.RequireHttpsMetadata = false;
            //    opts.SaveToken = true;
            //    opts.To
            //});
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<TeamHelperDBContext>();
                    DBInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.EnsureDbCreated();
            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = string.Empty;
                config.SwaggerEndpoint("swagger/v1/swagger.json","Team Helper API");
            }
            );
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}