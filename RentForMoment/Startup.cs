namespace RentForMoment
{

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using RentForMoment.Data;
    using RentForMoment.Infrastructure.Extensions;
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Services.Chiefs;
    using RentForMoment.Services.Statistics;
    using RentForMoment.Data.Models;
    using CarRentingSystem.Infrastructure.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddDbContext<RentForMomentDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RentForMomentDbContext>();

            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();

            services
                .AddControllersWithViews(option => 
                {
                    option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IChiefsService, ChiefsService>();
            services.AddTransient<IPersonProfilesService, PersonProfilesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error")
                   .UseHsts();
            }
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
