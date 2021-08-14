namespace RentForMoment.Tests.Controller.Pipeline
{
    using MyTested.AspNetCore.Mvc;
    using RentForMoment.Controllers;
    using Xunit;
    using RentForMoment.Models.Chiefs;
    using RentForMoment.Data.Models;
    using RentForMoment.Models.PersonProfiles;
    using System.Linq;

    using static WebConstants;

    public class ChiefsControllerTest
    {

        [Fact]
        public void RouteTest()
           => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Chiefs/Create")
                .WithUser())
            .To<ChiefsController>(c => c.Create())
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Chief", "+359123456789")]
        public void PostCreateShouldBeForAuthorizedUsersAndReturnRedirectWithValidView(
            string chiefsName,
            string phoneNumber)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Chiefs/Create")
                .WithMethod(HttpMethod.Post)
            .WithFormFields(new 
            {
                Name = chiefsName,
                PhoneNumber = phoneNumber
            })
                .WithUser()
            .WithAntiForgeryToken())
           .To<ChiefsController>(c => c.Create(new BecomeChiefViewModel
            {
                Name = chiefsName,
                PhoneNumber = phoneNumber
            }))
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
                .Data(data => data
                    .WithSet<Chief>(chiefs => chiefs
                    .Any(d =>
                    d.Name == chiefsName &&
                    d.PhoneNumber == phoneNumber &&
                    d.UserId == TestUser.Identifier)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<PersonProfilesController>(c => c.All(With.Any<AllPersonsProfileQueryModel>())));
    }
}
