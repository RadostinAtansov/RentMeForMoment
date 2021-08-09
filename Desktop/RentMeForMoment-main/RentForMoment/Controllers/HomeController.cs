namespace RentForMoment.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Models;
    using RentForMoment.Models.Home;
    using RentForMoment.Services.Statistics;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly RentForMomentDbContext data;
        private readonly IStatisticsService statistics;


        public HomeController(IStatisticsService statistics, RentForMomentDbContext data)
        {
            this.statistics = statistics;   
            this.data = data;
        }

        public IActionResult Index()
        {

            var TotalProfiles = this.data.PersonProfiles.Count();
            var TotalUsers = this.data.Users.Count();

            var profiles = data
                 .PersonProfiles
                 .OrderByDescending(p => p.Id)
                 .Select(p => new ProfileIndexViewModel
                 {
                     Firstname = p.FirstName,
                     Lastname = p.LastName,
                     Age = p.Years,
                     City = p.City,
                     Skills = p.Skills,
                     Image = p.PersonImage,
                     Description = p.Description
                 })
                 .Take(3)
                 .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalProfiles = totalStatistics.TotalProfiles,
                TotalUsers = totalStatistics.TotalUsers,
                Profiles = profiles
            });

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
