namespace RentForMoment.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using RentForMoment.Models.Home;
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Services.PersonProfiles.Models;
    using RentForMoment.Services.Statistics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IPersonProfilesService personProfiles;
        private readonly IStatisticsService statistics;
        private readonly IMemoryCache cache;

        public HomeController(
            IStatisticsService statistics,
            IPersonProfilesService personProfiles,
            IMemoryCache cache)
        {
            this.cache = cache;
            this.statistics = statistics;
            this.personProfiles = personProfiles;
        }

        public IActionResult Index()
        {

            const string LatestPersonProfilesCacheKey = "LatestPersonProfilesCacheKey";

            var latestPersonProfiles = this.cache.Get<List<LatestPersonProfileServiceModel>>(LatestPersonProfilesCacheKey);

            if (latestPersonProfiles == null)
            {
                latestPersonProfiles = this.personProfiles
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                this.cache.Set(LatestPersonProfilesCacheKey, latestPersonProfiles, cacheOptions);
            }

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalProfiles = totalStatistics.TotalProfiles,
                TotalUsers = totalStatistics.TotalUsers,
                Profiles = latestPersonProfiles
            });

        }

        
        public IActionResult Error() => View();
        
    }
}
