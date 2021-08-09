namespace RentForMoment.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
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
        private readonly IConfigurationProvider mapper;

        public HomeController(IStatisticsService statistics, 
            RentForMomentDbContext data,
            IMapper mapper)
        {
            this.mapper = mapper.ConfigurationProvider;
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
                 .ProjectTo<ProfileIndexViewModel>(this.mapper)
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
