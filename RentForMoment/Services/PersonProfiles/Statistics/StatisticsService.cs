namespace RentForMoment.Services.Statistics
{

    using RentForMoment.Data;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {

        private readonly RentForMomentDbContext data;

        public StatisticsService(RentForMomentDbContext data)
          =>  this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalProfiles = this.data.PersonProfiles.Count(p => p.IsPublic);
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalProfiles = totalProfiles,
                TotalUsers = totalUsers
            };
        }
    }
}
