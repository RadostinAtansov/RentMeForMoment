namespace RentForMoment.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using RentForMoment.Controllers;
    using Xunit;


    public class HomeControllerTest
    {
        [Fact]

        public void IndexRouteShoudBeMapper()
            => MyRouting
            .Configuration()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRoutShouldBeMapper()
            => MyRouting
            .Configuration()
            .ShouldMap("/home/Error")
            .To<HomeController>(c => c.Error());

    }
}
