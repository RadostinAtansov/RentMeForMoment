namespace RentForMoment.Tests.Controllers
{
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using RentForMoment.Controllers;
    using RentForMoment.Models.Home;
    using Xunit;

    using static Data.PersonProfiles;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
          => MyController<HomeController>
                       .Instance(instance => instance
                               .WithData(TenPublicPersonProfiles()))
                                .Calling(c => c.Index())
                       .ShouldReturn()
                       .View(view => view
                            .WithModelOfType<IndexViewModel>()
                            .Passing(m => m.Should()));

        //-------------- greshka nqkakva

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Error())
            .ShouldReturn()
            .View();
    }
}
