namespace RentForMoment.Tests.Controller
{
    using RentForMoment.Controllers;
    using RentForMoment.Data.Models;
    using RentForMoment.Models.Home;
    using RentForMoment.Services.PersonProfiles;
    using RentForMoment.Services.Statistics;
    using RentForMoment.Tests.Mocks;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {

        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
          =>  MyController<HomeController>
                       .Instance(controller => controller
                               .WithData(GetPersonProfiles()))
                       .Calling(c => c.Index())
                       .ShouldReturn()
                       .View(view => view
                            .WithModelOfType<IndexViewModel>()
                            .Passing(m => m.Profiles.Should().HaveCount(3)));

        [Fact]
        public void IndexShouldReturnViewWithCorrectViewModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var personProfiles = Enumerable.Range(0, 10).Select(i => new PersonProfile());

            data.PersonProfiles.AddRange(personProfiles);
            data.SaveChanges();

            data.Users.Add(new User());
            data.SaveChanges();

            var personProfileServices = new PersonProfilesService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(statisticsService, personProfileServices);
             
            //Act
            var result = homeController.Index();

            //Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Profiles.Count());
            Assert.Equal(10, indexViewModel.TotalProfiles);
            Assert.Equal(1, indexViewModel.TotalUsers);

        }
         


        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(null, null);
            // ACt
            var result = homeController.Error();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        private static IEnumerable<PersonProfile> GetPersonProfiles()
            => Enumerable.Range(0, 10).Select(i => new PersonProfile());

    }
}
