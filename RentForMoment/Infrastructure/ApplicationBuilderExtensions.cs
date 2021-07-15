namespace RentForMoment.Infrastructure
{

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentForMoment.Data;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {

            IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            using var scopedServices = serviceScope;

            var data = scopedServices.ServiceProvider.GetService<RentForMomentDbContext>();

            data.Database.Migrate();

            return app;
        }

    }
}
