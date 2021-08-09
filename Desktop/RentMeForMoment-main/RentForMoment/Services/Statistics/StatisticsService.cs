using RentForMoment.Data;
using System.Linq;

namespace RentForMoment.Services.Statistics
{



    public class StatisticsService : IStatisticsService
    {

        private readonly RentForMomentDbContext data;

        public StatisticsService(RentForMomentDbContext data)
          =>  this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalProfiles = this.data.PersonProfiles.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalProfiles = totalProfiles,
                TotalUsers = totalUsers
            };
        }
    }
}
