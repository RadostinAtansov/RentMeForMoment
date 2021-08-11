namespace RentForMoment.Tests.Controller.Api
{
    using RentForMoment.Controllers.Api;
    using RentForMoment.Tests.Mocks;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticksServiceMock.Instance);
            //Act
            var result = statisticsController.GetStatistics();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalProfiles);
            Assert.Equal(10, result.TotalRentForWork);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}
