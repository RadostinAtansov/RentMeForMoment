namespace RentForMoment.Tests.Routing
{

    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using RentForMoment.Controllers;
    using RentForMoment.Models.Chiefs;

    public class ChiefsControllerTest
    {
        [Fact]
        public void CreateShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Chiefs/Create")
            .To<ChiefsController>(c => c.Create());


        [Fact]
        public void PostCreateShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Chiefs/Create")
                .WithMethod(HttpMethod.Post))
            .To<ChiefsController>(c => c.Create(With.Any<BecomeChiefViewModel>()));
    }
}
