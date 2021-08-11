namespace RentForMoment.Tests.Services
{
    using RentForMoment.Data;
    using RentForMoment.Data.Models;
    using RentForMoment.Services.Chiefs;
    using RentForMoment.Tests.Mocks;
    using Xunit;

    public class ChiefsServicesTest
    {

        private const string userId = "TestUserId";


        [Fact]
        public void IsChiefShouldReturnTrueWhenUserIsChiefs()
        {
            //Arrange
            var chiefsService = GetChiefsService();

            //Act
            var result = chiefsService.IsChief(userId);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public void IsChiefShouldReturnFalseWhenUserIsNotChief()
        {
            //Arrange
            var chiefsService = GetChiefsService();

            //Act
            var result = chiefsService.IsChief("AnotherUserId");

            //Assert
            Assert.False(result);
        }

        private static IChiefsService GetChiefsService()
        {
            var data = DatabaseMock.Instance;

            data.Chiefs.Add(new Chief { UserId = userId });
            data.SaveChanges();

            return new ChiefsService(data);
        }

    }
}
