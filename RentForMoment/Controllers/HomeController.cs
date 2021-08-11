namespace RentForMoment.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Models.Home;
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Services.Statistics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IPersonProfilesService personProfiles;
        private readonly IStatisticsService statistics;

        public HomeController(
            IStatisticsService statistics,
            IPersonProfilesService personProfiles)
        {
            this.statistics = statistics;
            this.personProfiles = personProfiles;
        }

        public IActionResult Index()
        {

            var latestProfiles = this.personProfiles
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalProfiles = totalStatistics.TotalProfiles,
                TotalUsers = totalStatistics.TotalUsers,
                Profiles = latestProfiles
            });

        }

        
        public IActionResult Error() => View();
        
    }
}
