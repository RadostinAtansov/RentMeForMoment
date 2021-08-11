namespace RentForMoment.Tests.Mocks
{
    using Moq;
    using RentForMoment.Services.Statistics;

    public static class StatisticksServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalProfiles = 5,
                        TotalRentForWork = 10,
                        TotalUsers = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
