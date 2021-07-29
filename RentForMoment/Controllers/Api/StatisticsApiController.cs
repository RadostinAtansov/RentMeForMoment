namespace RentForMoment.Controllers.Api
{

    using Microsoft.AspNetCore.Mvc;
    using RentForMoment.Data;
    using RentForMoment.Models.Api;
    using System.Linq;

    [ApiController]
    [Route("api/statistics")]

    public class StatisticsApiController : ControllerBase
    {

        private readonly RentForMomentDbContext data;

        public StatisticsApiController(RentForMomentDbContext data)
            => this.data = data;

        [HttpGet]

        public StatisticsResponseModel Get()
        {

            var statistics = new StatisticsResponseModel
            {
                TotalProfiles = this.data.PersonProfiles.Count(),
                TotalUsers = this.data.Users.Count(),
                TotalRentForWork = 0
            };

            return statistics;
        }

    }
}
