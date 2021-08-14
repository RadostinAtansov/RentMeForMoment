namespace RentForMoment.Tests.Controller.Pipeline
{
    using RentForMoment.Controllers;
    using RentForMoment.Models.Home;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.PersonProfiles;

    public class HomeControllerTest
    {

        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
          =>  MyController<HomeController>
                       .Instance(controller => controller
                               .WithData(TenPublicPersonProfiles()))
                       .Calling(c => c.Index())
                       .ShouldReturn()
                       .View(view => view
                            .WithModelOfType<IndexViewModel>()
                            .Passing(m => m.Profiles.Should().HaveCount(3)));


        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
            .Pipeline()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error())
            .Which()
            .ShouldReturn()
            .View();
    }
}
