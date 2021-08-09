namespace RentForMoment.Infrastructure
{

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using System;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {

            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<RentForMomentDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(RentForMomentDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Physical"},
                new Category { Name = "Mental"},
                new Category { Name = "Physical and Mental"}
            });

            data.SaveChanges();
        }
    }
}
